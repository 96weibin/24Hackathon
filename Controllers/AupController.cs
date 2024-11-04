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
using AIQuestionAnswer.GraphQL;

namespace AIQuestionAnswer.Controllers
{
    [RoutePrefix("api/aup")]
    public class AupController : ApiController
    {
        public AupController() { }

        [Route("FindTopMargin")]
        [HttpPost]
        public FindTopResponse FindTopMargin([FromBody] Intent intent)
        {
            var result = new FindTopResponse();

            // get modelName from last solution if it is missing from Intent.
            if (string.IsNullOrEmpty(intent.ModelName))
            {
                intent.ModelName = DbContext.GetLastOptimizeModelName();
            }

            //get case name if it is not provided
            if (string.IsNullOrEmpty(intent.CaseName)) 
            {
                intent.CaseName = DbContext.GetOneCaseOfLastOptimizeModel(intent.ModelName);
 
            }
            result.Intent = intent; 

            result.Margins = DbContext.GetVariableMargins(intent);

            return result;
        }


        [Route("AdjustMargin")]
        [HttpPost]
        public async Task<AdjustMarginResponse> AdjustMargin([FromBody] Intent intent)
        {
            var result = new AdjustMarginResponse();

            // find top margins variables from  DB
            var topMarginResponse =  FindTopMargin(intent);
            result.Intent = topMarginResponse.Intent; // update Intent from FindTopMargin for default model and case name.

            /// call AUP GraphQL API for a temp case .
            AupGraphQLClient client = new AupGraphQLClient();
            string newCaseName = $"{result.Intent.CaseName}_Adjusted{DateTime.Now.TimeOfDay.Milliseconds}";
            // 1. add a new case by relax the bound for the top margin variables
            await client.AddNewCaseByAdjustMargins(topMarginResponse.Margins, result.Intent.ModelName, result.Intent.CaseName, newCaseName);
            // 2. create a job to run the new case
            string runCaseJobId = await client.RunCase(result.Intent.ModelName, newCaseName);
            // 3. wait for job finish.
            await client.WaitForRunCaseJob(result.Intent.ModelName, runCaseJobId, 2);
            
            //read the obj of original case and new case from database
            (result.Obj1, result.Obj2) = DbContext.CompareTwoCasesObj(intent.CaseName, newCaseName, intent.ModelName);

            return result;
        }

    }
}