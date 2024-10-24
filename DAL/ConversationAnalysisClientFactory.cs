using Azure;
using Azure.AI.Language.Conversations;
using Azure.AI.Language.QuestionAnswering;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Json;
using System.Web;
using System.Windows.Input;

namespace knowledgeBase.DAL
{
    public class ConversationAnalysisClientFactory
    {
        private static string _url = ConfigurationManager.AppSettings["AzureCLUUrl"]; 
        private static string _key = ConfigurationManager.AppSettings["AzureCLUKey"];

        private static ConversationAnalysisClient _client;
 
        public static ConversationAnalysisClient GetClient()
        {
            if(_client == null)
            {
                Uri endpoint = new Uri(_url);
                AzureKeyCredential credential = new AzureKeyCredential(_key);
                _client = new ConversationAnalysisClient(endpoint, credential);
            }
            return _client;
        }



    }
}