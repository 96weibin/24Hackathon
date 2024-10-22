using Azure;
using Azure.AI.Language.Conversations;
using Azure.AI.Language.QuestionAnswering;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
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
        private static string _url = "https://langcreatedinus.cognitiveservices.azure.com";
        private static string _key = "c1c2002fbc0f4af69af1b852d6945293";

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