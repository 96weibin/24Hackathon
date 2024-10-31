﻿using GraphQL.Types.Relay.DataObjects;
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

            if (string.IsNullOrEmpty(intent.ModelName)) // get modelName from last solution if it is missing from Intent.
            {
                intent.ModelName = DbContextFactory.GetLastOptimizeModelName();
            }

            if(string.IsNullOrEmpty(intent.CaseName)) //get case name if it is not provided
            {
                intent.CaseName = DbContextFactory.GetOneCaseOfLastOptimizeModel(intent.ModelName);
 
            }
            result.Intent = intent; 

            result.Margins = DbContextFactory.GetVariableMargins(intent);

            return result;
        }


        [Route("AdjustMargin")]
        [HttpPost]
        public async Task<AdjustMarginResponse> AdjustMargin([FromBody] AdjustMarginRequest request)
        {
            var result = new AdjustMarginResponse();

            var topMarginResponse =  FindTopMargin(request.intent);

            result.Intent = topMarginResponse.Intent; // update Intent from FindTopMargin for default model and case name.

            //AupGraphQLClient client = new AupGraphQLClient();
            //client.AddNewCaseByAdjustMargins(result.Margins, result.Intent.ModelName, result.Intent.CaseName);

            request.CaseName2 = request.CaseName1;

            (result.Obj1, result.Obj2) = DbContextFactory.CompareTwoCasesObj(request.CaseName1, request.CaseName2, request.intent.ModelName);

            request.intent.CaseName = request.CaseName2;
            result.Margins = DbContextFactory.GetVariableMargins(request.intent);
            return result;
        }

    }
}