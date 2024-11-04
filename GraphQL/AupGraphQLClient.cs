using AIQuestionAnswer.DAL;
using AIQuestionAnswer.DataContract;
using Azure.Core;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RtfPipe.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AIQuestionAnswer.GraphQL
{
    public class AupGraphQLClient
    {
        private static string _apiurl = ConfigurationManager.AppSettings["AuAPIEndpoint"];
        private readonly double _absBias = 0.5; // use when the bound is null or empty.
        private readonly double _relativeBias = 0.05; // adjust by 5% of original value.

        private string GetUpdateFunName(NonBasisType nonBasisType)
        {
            var result = "";
            switch (nonBasisType)
            {
                case NonBasisType.Purchase:
                    result = "updatePurchases";
                    break;
                case NonBasisType.Sales:
                    result = "updateSales";
                    break;
                case NonBasisType.ProcLimit:
                    result = "updateProcessLimits";
                    break;
                case NonBasisType.Capacity:
                    result = "updateCapacities";
                    break;
            }
            return result;
        }

        private double GetAdjustLimit(VariableMargin margin)
        {
            if(margin.Margin > 0)
            {
                return margin.HighBound == 0? _absBias : margin.HighBound * (1+_relativeBias);
            }
            else
            {
                return margin.LowBound * (1-_relativeBias);
            }
        }
        public async Task AddNewCaseByAdjustMargins(List<VariableMargin> margins, string modelName, string parentCaseName, string newCaseName)
        {
            string queryName = "mutation CreateCase($name: String!, $newCase: String!";
            int index = 0;
            foreach(var margin in margins)
            {
                queryName += string.Format(", $var{0}:String!, $limit{0}:Float!", index);
                index++;

            }
            queryName += ")";

            StringBuilder query = new StringBuilder();
            query.AppendLine(queryName);
            query.Append("{\n\tcases{\n\t\tadd(input:{\n\t\t\tname:$newCase\n\t\t\tparentCaseName:$name\n\t\t})\n\t{");
         

            var marginsOrderInType = margins.OrderBy(p => p.NonBasisType).ToList();
            NonBasisType nonBasisTypeFlag = NonBasisType.All;
            index = 0;
            foreach (var margin in marginsOrderInType)
            {
                if(margin.NonBasisType != nonBasisTypeFlag)
                {
                    if(nonBasisTypeFlag != NonBasisType.All)
                    {
                        query.Append("])\n{\nid\n}\n");
                    }
                    query.Append(GetUpdateFunName(margin.NonBasisType));
                    query.Append("(inputs:[\n");
                    nonBasisTypeFlag = margin.NonBasisType;
                }
                else
                {
                    query.AppendLine(",");
                }
                query.AppendLine("{");
                query.AppendLine(string.Format("name:$var{0}", index));
                query.AppendLine("inputs:[");
                query.AppendLine("{");
                query.AppendLine(string.Format("field:{0}", margin.Margin > 0? "Max":"Min"));
                query.AppendLine(string.Format("value:$limit{0}", index));
                query.Append("}\n]\n}\n");
                index++;
            }
            query.Append("])\n{\nid\n}\n}\n}\n}");

            dynamic variables = new ExpandoObject();
            variables.name = parentCaseName;
            variables.newCase = newCaseName;

            index = 0;
            foreach (var margin in marginsOrderInType)
            {
                ((IDictionary<string, object>)variables).Add($"var{index}", margin.VariableName);
                ((IDictionary<string, object>)variables).Add($"limit{index}", GetAdjustLimit(margin));
                index++;
            }

            var requestContent = new GraphQLRequest
            {
                Query = query.ToString(),
                Variables = variables
            };

           await  ExecuteRequest(modelName, requestContent);

        }

        
        public async Task<string> RunCase(string modelName, string caseName)
        {
            string query = @"mutation{
                runCases: caseExecution {
                  submitCaseStack(
                    input:{
                      name: ""Job""
                      cases: [
                        {name: """ + caseName +  @"""}
                      ]
                    }
                  )
                  {
                    id
                  }
                }
            }";

           var response =  await ExecuteRequest(modelName, new GraphQLRequest(query));
            JObject jsonObject = (JObject)response;
            return jsonObject["runCases"]["submitCaseStack"]["id"].ToString();
        }

        public async Task WaitForRunCaseJob(string modelName, string jobId, float maximumMinutesToWait)
        {
            string query = @"mutation  waitJob($id:String!,$minutes:Float!){
                  caseExecution {
                  waitForCaseStackJob(id:$id, name: ""Job"", minutesToWait: $minutes)
                  {
                    id
                  }
                }
            }";
            var variables = new
            {
                id = jobId,
                minutes = maximumMinutesToWait
            };

            await ExecuteRequest(modelName, new GraphQLRequest()
            {
                Query = query,
                Variables = variables
            });

        }
        private async Task<dynamic> ExecuteRequest(string modelName, GraphQLRequest request)
        {

            var username = "Administrator";
            var password = "Aspen100";

            using (var httpClientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(username, password)
            })

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var graphQLClient = new GraphQLHttpClient(GetGraphUrlForModel(modelName), new NewtonsoftJsonSerializer(), httpClient);    
                var response = await graphQLClient.SendQueryAsync<dynamic>(request);
                return response.Data;
            }
        }


        private string GetGraphUrlForModel(string modelName)
        {
            return $"{_apiurl}/model/{modelName}/graphql";
        }

    }
}