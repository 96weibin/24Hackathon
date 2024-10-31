using AIQuestionAnswer.DAL;
using AIQuestionAnswer.DataContract;
using Azure.Core;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public void AddNewCaseByAdjustMargins(List<VariableMargin> margins, string modelName, string caseName)
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
            query.Append("{\n\tcases\n\t\tadd(input:{\n\t\t\tname:$newCase\n\t\t\tparentCaseName:$name\n\t\t})\n\t{");
         

            var marginsOrderInType = margins.OrderBy(p => p.NonBasisType).ToList();
            NonBasisType nonBasisTypeFlag = NonBasisType.All;
            index = 0;
            foreach (var margin in marginsOrderInType)
            {
                if(margin.NonBasisType != nonBasisTypeFlag)
                {
                    if(margin.NonBasisType != NonBasisType.All)
                    {
                        query.Append("])\n");
                    }
                    query.Append(GetUpdateFunName(margin.NonBasisType));
                    query.Append("(inputs:[\n");
                    nonBasisTypeFlag = margin.NonBasisType;
                }
                query.AppendLine("{");
                query.AppendLine(string.Format("name:$var{0}", index));
                query.AppendLine("inputs:[");
                query.AppendLine("{");
                query.AppendLine(string.Format("field:{0}", margin.Margin > 0? "Max":"Min"));
                query.AppendLine(string.Format("value:$limit{0}", index));
                query.AppendLine("]");
                index++;
            }
            query.Append("])\n}\n}\n}\n");

            dynamic variables = new ExpandoObject();
            variables.name = caseName;
            variables.newCase = $"{caseName}_Adjusted";

            index = 0;
            foreach (var margin in marginsOrderInType)
            {
                ((IDictionary<string, object>)variables).Add($"var{index}", margin.VariableName);
                ((IDictionary<string, object>)variables).Add($"limit{index}", GetAdjustLimit(margin));
                index++;
            }

            var requstContent = new GraphQLRequest
            {
                Query = query.ToString(),
                Variables = variables
            };


            string graphUrl = GetModelUrl(modelName);
            var ret = Execute(modelName, requstContent).Result;

        }
        public async Task<dynamic> Execute(string modelName, GraphQLRequest request)
        {
            var client = new GraphQLHttpClient(GetModelUrl(modelName), new NewtonsoftJsonSerializer());

            var username = "Administrator";
            var password = "Aspen100";
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);


            var response = await client.SendQueryAsync<dynamic>(request);

            return response;
        }


        private string GetModelUrl(string modelName)
        {
            return $"{_apiurl}/model/{modelName}/graphql";
        }

        public string BuildMutation(
                CaseInput caseInput,
                List<UpdateVaribelInput> salesInputs = null,
                List<UpdateVaribelInput> processLimitsInputs = null,
                List<UpdateVaribelInput> purchaseInputs = null,
                List<UpdateVaribelInput> capacitiesInputs = null)
        {
            string mutation = $@"
            mutation {{
                cases {{
                    add(input: {{
                        name: ""{caseInput.Name}""
                        parentCaseName: ""{caseInput.ParentCaseName}""
                        }}) {{
                   name";
            if (salesInputs != null && salesInputs.Count > 0)
            {
                //wrong
                string salesInputsJson = JsonConvert.SerializeObject(salesInputs);
                mutation += $@"
                   updateSales(inputs: {salesInputsJson}) {{
                       id
                   }}";
            }
            if (processLimitsInputs != null && processLimitsInputs.Count > 0)
            {
                string processLimitsJson = JsonConvert.SerializeObject(processLimitsInputs);
                mutation += $@"
                   updateProcessLimits(inputs: {processLimitsJson}) {{
                       id
                   }}";
            }
            if (purchaseInputs != null && purchaseInputs.Count > 0)
            {
                string purchaseInputsJson = JsonConvert.SerializeObject(purchaseInputs);
                mutation += $@"
                   updatePurchases(inputs: {purchaseInputsJson}) {{
                       id
                   }}";
            }
            if (capacitiesInputs != null && capacitiesInputs.Count > 0)
            {
                string capacitiesInputsJson = JsonConvert.SerializeObject(capacitiesInputs);
                mutation += $@"
                   updateCapacities(inputs: {capacitiesInputsJson}) {{
                       id
                   }}";
            }
            // 关闭 mutation
            mutation += @"
                }
            }
            }";
            return mutation;
        }


        public class Type
        {
            public string Format
            {
                get;
                private set;
            }

            private Type(string format)
            {
                Format = format + ":\"\"";
            }

            public readonly static Type FromName = new Type("fromName");
            public readonly static Type ToName = new Type("toName");
            public readonly static Type PlantName = new Type("plantName");
            public readonly static Type JobName = new Type("name");
            public readonly static Type CaseName = new Type("name");
            public readonly static Type Field = new Type("field");

        }

        public class CaseInputRequest
        {
            public CaseInput caseInput { get; set; }
            public List<UpdateVaribelInput> salesInputs { get; set; }
            public List<UpdateVaribelInput> processLimitsInputs { get; set; }
            public List<UpdateVaribelInput> purchaseInputs { get; set; }
            public List<UpdateVaribelInput> capacitiesInputs { get; set; }
        }


        public class CaseInput
        {
            public string Name { get; set; }
            public string ParentCaseName { get; set; }
        }

        public class UpdateVaribelInput
        {
            public string Name { get; set; }
            public List<FieldInput> Inputs { get; set; }
        }

        public class FieldInput
        {
            public string Field { get; set; }
            public double Value { get; set; }
        }

    }
}