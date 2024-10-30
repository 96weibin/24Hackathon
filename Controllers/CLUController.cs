
using System.Web.Http;
using Azure.Core;
using Azure.Core.Serialization;
using Azure.AI.Language.Conversations;
using Azure;
using System;
using System.Text.Json;
using AIQuestionAnswer.DAL;
using AIQuestionAnswer.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace AIQuestionAnswer.Controllers
{
    [RoutePrefix("api/ai")]
    public class CLUController : ApiController
    {
        private string _projectName = ConfigurationManager.AppSettings["AzureCLUProject"]; 
        private string _deploymentName = ConfigurationManager.AppSettings["AzureCLUDeploy"];

        [Route("intent")]
        [HttpPost]
        public async Task<Intent> PredictUserIntent([FromBody] string userInput)
        {
            ConversationAnalysisClient client = ConversationAnalysisClientFactory.GetClient();
            var data = new
            {
                analysisInput = new
                {
                    conversationItem = new
                    {
                        text = userInput,
                        id = "1",
                        participantId = "1",
                    }
                },
                parameters = new
                {
                    projectName =  _projectName,
                    deploymentName = _deploymentName,

                    // Use Utf16CodeUnit for strings in .NET.
                    stringIndexType = "Utf16CodeUnit",
                },
                kind = "Conversation",
            };

            Response response = await client.AnalyzeConversationAsync(RequestContent.Create(data));

             JsonDocument result = JsonDocument.Parse(response.ContentStream);
            JsonElement conversationalTaskResult = result.RootElement;
            JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");
            Intent intent = ParseIntentFromPredictJson(conversationPrediction);
            intent.Question = userInput;
            return intent;
        }


        private  Intent ParseIntentFromPredictJson(JsonElement conversationPrediction)
        {
            Intent intent = new Intent();
            string topIntent = conversationPrediction.GetProperty("topIntent").GetString();
            switch (topIntent)
            {
                case "FindTopMargin":
                    intent.Category = Category.FindTopMargin;
                    break;
                case "AdjustMargin":
                    intent.Category = Category.AdjustMargin;
                    break;
                case "None":
                default:
                    intent.Category = Category.None;
                    return intent;
            }


            foreach (JsonElement intentEle in conversationPrediction.GetProperty("intents").EnumerateArray())
            {
                if (intentEle.GetProperty("category").GetString() == topIntent)
                {
                    intent.ConfidenceScore = intentEle.GetProperty("confidenceScore").GetSingle();
                }
            }

            foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
            {
                string entityName = entity.GetProperty("category").GetString();
                string entityValue = entity.GetProperty("text").GetString();
                switch (entityName)
                {
                    case "CaseName":
                        intent.CaseName = entityValue;
                        break;
                    case "ModelName":
                        intent.ModelName = entityValue;
                        break;
                    case "NonBasisType":
                        if (entityValue.StartsWith("purchase", StringComparison.OrdinalIgnoreCase) ||
                            entityValue.StartsWith("crude oils", StringComparison.OrdinalIgnoreCase))
                            intent.NonBasisType = NonBasisType.Purchase;
                        else if (entityValue.StartsWith("sell", StringComparison.OrdinalIgnoreCase)
                            || entityValue.StartsWith("sales", StringComparison.OrdinalIgnoreCase))
                            intent.NonBasisType = NonBasisType.Sales;
                        else if (entityValue.StartsWith("capacity", StringComparison.OrdinalIgnoreCase))
                            intent.NonBasisType = NonBasisType.Capacity;
                        else if (entityValue.StartsWith("process limit", StringComparison.OrdinalIgnoreCase))
                            intent.NonBasisType = NonBasisType.ProcLimit;
                        else if (entityValue.StartsWith("operations parameters", StringComparison.OrdinalIgnoreCase))
                            intent.NonBasisType = NonBasisType.Operation;
                        break;
                    case "TopNumber":
                        if (int.TryParse(entityValue, out int value))
                        {
                            intent.TopNumber = value;
                        }
                        else
                        {
                            intent.TopNumber = ConvertWordToNumber(entityValue);
                        }
                        break;
                }
            };

            return intent;
        }

        int ConvertWordToNumber(string word)
        {
            // Dictionary to hold the word-number mapping
            Dictionary<string, int> wordToNumberMap = new Dictionary<string, int>{
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9},
            };

            // Make the input word lowercase for comparison
            word = word.ToLower();

            // Check if the word is in the dictionary
            if (wordToNumberMap.ContainsKey(word))
            {
                return wordToNumberMap[word];
            }
            else
            {
                return 0;
            }
        }
    }
}
