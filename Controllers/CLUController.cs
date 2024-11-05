
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
        private string _url = ConfigurationManager.AppSettings["AzureCLUUrl"];
        private string _key = ConfigurationManager.AppSettings["AzureCLUKey"];
        private string _projectName = ConfigurationManager.AppSettings["AzureCLUProject"]; 
        private string _deploymentName = ConfigurationManager.AppSettings["AzureCLUDeploy"];
        /// <summary>
        /// The ConversationAnalysis client -- [Azure.AI.Language.Conversations.ConversationAnalysisClient]
        /// </summary>
        private static ConversationAnalysisClient _client;

        [Route("intent")]
        [HttpPost]
        public async Task<Intent> PredictUserIntent([FromBody] string userInput)
        {
            // create or get the Azure ConversationAnalysis client
            ConversationAnalysisClient client = GetAzureConversationAnalysisClient();
            // build the input data with user input string 
            var data = new
            {
                analysisInput = new
                {
                    conversationItem = new
                    {
                        text = userInput, //user input string
                        id = "1",
                        participantId = "1",
                    }
                },
                parameters = new
                {
                    projectName = _projectName,
                    deploymentName = _deploymentName,
                    stringIndexType = "Utf16CodeUnit",
                },
                kind = "Conversation",
            };

            // Analyze Conversation 
            Response response = await client.AnalyzeConversationAsync(RequestContent.Create(data));

            //get the result from the response  
            JsonDocument result = JsonDocument.Parse(response.ContentStream);
            JsonElement conversationalTaskResult = result.RootElement;
            JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");
            // parse the result Json data to Intent class which defined in this project.
            Intent intent = ParseIntentFromPredictJson(conversationPrediction);
            intent.Question = userInput; // pass  user input into Intent.Question field
            return intent;
        }


        private ConversationAnalysisClient GetAzureConversationAnalysisClient()
        {
            if (_client == null)
            {
                Uri endpoint = new Uri(_url);
                AzureKeyCredential credential = new AzureKeyCredential(_key);
                _client = new ConversationAnalysisClient(endpoint, credential);
            }
            return _client;
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
