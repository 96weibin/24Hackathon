using GraphQL.Types.Relay.DataObjects;
using AIQuestionAnswer.DAL;
using AIQuestionAnswer.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace AIQuestionAnswer.Controllers
{
    [RoutePrefix("api/aupdb")]
    public class ResultsDBController : ApiController
    {

        public ResultsDBController() { }

        [Route("FindTopMargin")]
        [HttpPost]
        public FindTopResponse FindTopMargin([FromBody] Intent intent)
        {
            var result = new FindTopResponse();

            var conn = DbContextFactory.GetOpenSqlConnection();

            if (string.IsNullOrEmpty(intent.ModelName)) // get modelName from last solution if it is missing from Intent.
            {
                using (SqlCommand command = new SqlCommand("GetDefaultModel", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add an output parameter to the command
                    SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                    modelNameParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(modelNameParameter);
                    command.ExecuteNonQuery();
                    // Read the output parameter value
                    intent.ModelName = modelNameParameter.Value.ToString();
                }
            }

            if(string.IsNullOrEmpty(intent.CaseName)) //get case name if it is not provided
            {
                string sqlText = "select Top 1 CaseName from RW_CaseDetails where NodeName = @ModelName order by SolutionID DESC";
                using (SqlCommand command = new SqlCommand(sqlText, conn))
                {
                    command.Parameters.AddWithValue("@ModelName", intent.ModelName);
                    intent.CaseName = command.ExecuteScalar().ToString();
                }
 
            }
            result.Intent = new Intent()
            {
                CaseName = intent.CaseName,
                ModelName = intent.ModelName
            };
            result.Margins = new List<VariableMargin>();
            using (SqlCommand command = new SqlCommand("GetTopBasis", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                modelNameParameter.Direction = ParameterDirection.Input;
                modelNameParameter.Value = intent.ModelName;
                SqlParameter caseNameParameter = new SqlParameter("@CaseName", SqlDbType.NVarChar, 100);
                caseNameParameter.Direction = ParameterDirection.Input;
                caseNameParameter.Value = intent.CaseName;
                SqlParameter topNumberParameter = new SqlParameter("@TopCount", SqlDbType.Int);
                topNumberParameter.Direction = ParameterDirection.Input;
                topNumberParameter.Value = intent.TopNumber;
                SqlParameter basisTypeParameter = new SqlParameter("@BasisType", SqlDbType.Int);
                basisTypeParameter.Direction = ParameterDirection.Input;
                basisTypeParameter.Value = (int)intent.NonBasisType;

                command.Parameters.Add(modelNameParameter);
                command.Parameters.Add(caseNameParameter);
                command.Parameters.Add(topNumberParameter);
                command.Parameters.Add(basisTypeParameter);
               

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var val = new VariableMargin();
                        val.VariableName = reader["BasisFactor"].ToString();
                        val.Margin = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
                        val.LowBound = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                        val.HighBound = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);
                        val.NonBasisType = GetNonBasis(reader["Basis"].ToString());
                        result.Margins.Add(val);
                    }
                }

                // Read the output parameter value
                intent.ModelName = modelNameParameter.Value.ToString();
            }
            return result;
        }

        private NonBasisType GetNonBasis(string str)
        {
            NonBasisType ret = NonBasisType.All;
            switch (str)
            {
                case "PURC":
                    return ret = NonBasisType.Purchase;
                case "SELL":
                    return ret = NonBasisType.Sales;
                case "CCAP":
                    return ret = NonBasisType.Capacity;
                case "Limt":
                    return ret = NonBasisType.ProcLimit;
                default:
                    return ret;
            }
        }

        [Route("AdjustMargin")]
        [HttpPost]
        public async Task<AdjustMarginResponse> AdjustMargin([FromBody] AdjustMarginRequest request)
        {
            var topRes =  FindTopMargin(request.intent);
            //topRes.Margins[1].HighBound = 61;
            var graphQlController = new GraphQLController();
            var caseInput = new GraphQLController.CaseInput() { Name="aa",ParentCaseName="Base Model"};
            var input = new GraphQLController.FieldInput() { Field = "Max", Value = 61 };
            var list = new List<GraphQLController.FieldInput>();
            var capacitiesInputs = new List<GraphQLController.UpdateVaribelInput>();

            list.Add(input);
            var capacitiesInput = new GraphQLController.UpdateVaribelInput() { Name = "Cat Cracker BPD", Inputs = list };
            capacitiesInputs.Add(capacitiesInput);
            var addNewCase = "mutation\n{\n  cases\n  {\n    add(input:{\n      name:\"FFE\"\n      parentCaseName:\"Base Model\"\n    })\n    {\n      name\n      updateCapacities(inputs:[\n        {\n          name:\"Cat Cracker BPD\",\n          inputs:[\n            {\n              field:Max\n              value:65\n            }\n          ]\n        }\n      ])\n      {\n        id\n      }\n    }\n  }\n}";
            var runNewCase = "mutation{\n    runCases: caseExecution {\n      submitCaseStack(\n        input:{\n          name: \"Job\"\n          cases: [\n            {name: \"FFE\"}\n          ]\n        }\n      )\n      {\n        id\n      }\n    }\n}";
                //graphQlController.BuildMutation(caseInput, null, null,null, capacitiesInputs);
            var ret = await graphQlController.Execute(request.intent.ModelName, addNewCase);
            var ret1 = await graphQlController.Execute(request.intent.ModelName, runNewCase);
            request.CaseName2 = "FFE";
            var result = new AdjustMarginResponse();

            var conn = DbContextFactory.GetOpenSqlConnection();

            if (string.IsNullOrEmpty(request.intent.ModelName)) // get modelName from last solution if it is missing from Intent.
            {
                using (SqlCommand command = new SqlCommand("GetDefaultModel", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add an output parameter to the command
                    SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                    modelNameParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(modelNameParameter);

                    await command.ExecuteReaderAsync();

                    // Read the output parameter value
                    request.intent.ModelName = modelNameParameter.Value.ToString();
                }
            }
            result.Intent = new Intent()
            {
                CaseName = request.intent.CaseName,
                ModelName = request.intent.ModelName
            };
            using (SqlCommand command = new SqlCommand("CompareObjInTwoCases", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                modelNameParameter.Direction = ParameterDirection.Input;
                modelNameParameter.Value = request.intent.ModelName;
                SqlParameter caseName1Parameter = new SqlParameter("@CaseName1", SqlDbType.NVarChar, 100);
                caseName1Parameter.Direction = ParameterDirection.Input;
                caseName1Parameter.Value = request.CaseName1;
                SqlParameter caseName2Parameter = new SqlParameter("@CaseName2", SqlDbType.NVarChar, 100);
                caseName2Parameter.Direction = ParameterDirection.Input;
                caseName2Parameter.Value = request.CaseName2;
                SqlParameter obj1Parameter = new SqlParameter("@Obj1", SqlDbType.Float, 100);
                obj1Parameter.Direction = ParameterDirection.Output;
                SqlParameter obj2Parameter = new SqlParameter("@Obj2", SqlDbType.Float, 100);
                obj2Parameter.Direction = ParameterDirection.Output;

                command.Parameters.Add(modelNameParameter);
                command.Parameters.Add(caseName1Parameter);
                command.Parameters.Add(caseName2Parameter);
                command.Parameters.Add(obj1Parameter);
                command.Parameters.Add(obj2Parameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    result.Obj1 = (double)obj1Parameter.Value;
                    result.Obj2 = (double)obj2Parameter.Value;
                }
            }
            return result;
        }

    }
}