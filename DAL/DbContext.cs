using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AIQuestionAnswer.DataContract;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Http.Results;
using Azure.Core;
using System.Drawing;

namespace AIQuestionAnswer.DAL
{
    public class DbContext
    {        
        private static string _sqlConStr = ConfigurationManager.ConnectionStrings["AupResultsDB"].ConnectionString;

  

        public static string GetLastOptimizeModelName()
        {
            using (var con = new SqlConnection(_sqlConStr))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("GetDefaultModel", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add an output parameter to the command
                    SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                    modelNameParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(modelNameParameter);
                    command.ExecuteNonQuery();
                    // Read the output parameter value
                    return modelNameParameter.Value.ToString();
                }
            }
        }

        public static string GetOneCaseOfLastOptimizeModel(string modelName)
        {
            string sqlText = "select Top 1 CaseName from RW_CaseDetails where NodeName = @ModelName order by SolutionID DESC";

            using (var con = new SqlConnection(_sqlConStr))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlText, con))
                {
                    command.Parameters.AddWithValue("@ModelName", modelName);
                    return command.ExecuteScalar().ToString();
                }
            }
        }

        public static List<VariableMargin> GetVariableMargins(Intent intent)
        {
            var result = new List<VariableMargin>();
            using (var con = new SqlConnection(_sqlConStr))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("GetTopBasis", con))
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
                            result.Add(val);
                        }
                    }
                }
            }
            return result;

            NonBasisType GetNonBasis(string str)
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
        public static (double obj1, double obj2) CompareTwoCasesObj(string case1, string case2, string modelName)
        {
            using (var con = new SqlConnection(_sqlConStr))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("CompareObjInTwoCases", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter modelNameParameter = new SqlParameter("@ModelName", SqlDbType.NVarChar, 100);
                    modelNameParameter.Direction = ParameterDirection.Input;
                    modelNameParameter.Value = modelName;
                    SqlParameter caseName1Parameter = new SqlParameter("@CaseName1", SqlDbType.NVarChar, 100);
                    caseName1Parameter.Direction = ParameterDirection.Input;
                    caseName1Parameter.Value = case1;
                    SqlParameter caseName2Parameter = new SqlParameter("@CaseName2", SqlDbType.NVarChar, 100);
                    caseName2Parameter.Direction = ParameterDirection.Input;
                    caseName2Parameter.Value = case2;
                    SqlParameter obj1Parameter = new SqlParameter("@Obj1", SqlDbType.Float, 100);
                    obj1Parameter.Direction = ParameterDirection.Output;
                    SqlParameter obj2Parameter = new SqlParameter("@Obj2", SqlDbType.Float, 100);
                    obj2Parameter.Direction = ParameterDirection.Output;

                    command.Parameters.Add(modelNameParameter);
                    command.Parameters.Add(caseName1Parameter);
                    command.Parameters.Add(caseName2Parameter);
                    command.Parameters.Add(obj1Parameter);
                    command.Parameters.Add(obj2Parameter);

                    command.ExecuteNonQuery();

                    return ((double)obj1Parameter.Value, (double)obj2Parameter.Value);
                }
            }
        }
    }
}
