using GraphQL.Types.Relay.DataObjects;
using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/aupdb")]
    public class ResultsDBController : ApiController
    {

        public ResultsDBController() { }

        [Route("FindTopMargin")]
        [HttpPost]
        public async Task<FindTopResponse> FindTopMargin([FromBody] Intent intent)
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

                    await command.ExecuteReaderAsync();

                    // Read the output parameter value
                    intent.ModelName = modelNameParameter.Value.ToString();
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
                basisTypeParameter.Value = intent.NonBasisType;

                command.Parameters.Add(modelNameParameter);
                command.Parameters.Add(caseNameParameter);
                command.Parameters.Add(topNumberParameter);
                command.Parameters.Add(basisTypeParameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Margins.Add(new VariableMargin()
                        {
                            VariableName = reader["BasisFactor"].ToString(),
                            Margin = (double)reader["Margin"],
                            LowBound = (double)reader["LoBound"],
                            HighBound = (double)reader["HiBound"],
                            NonBasisType = GetNonBasis(reader["Basis"].ToString())
                        });
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

    }
}