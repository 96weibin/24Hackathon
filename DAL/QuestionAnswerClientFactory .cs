using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;

namespace knowledgeBase.DAL
{
    public class QuestionAnswerClientFactory
    {
        private static readonly AzureKeyCredential credential = new AzureKeyCredential("df7c5ee15359493da03cbf006acf902a");
        private static readonly Uri endpoint = new Uri("https://questionansweringbot.cognitiveservices.azure.com/");
        private static string deploymentName = "production";

        public static QuestionAnsweringClient GetQuestionAnsweringClient()
        {
            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
            return client;

        }
        
        public static QuestionAnsweringProject GetQuestionAnsweringProject(string projectName)
        {
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);
            return project;
        }
    } 
       
}
