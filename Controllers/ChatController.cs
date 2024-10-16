using Azure;
using Azure.AI.Language.QuestionAnswering;
using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using knowledgeBase.Models;
using Newtonsoft.Json;
using RtfPipe.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Comment = knowledgeBase.Models.Comment;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/Chat")]

    public class ChatController : ApiController
    {

        [Route("GetChatToken/{familyId}/{userId}")]
        public async Task<string> GetChatTokenAsync(Family familyId, int userId)
        {
            string secret = "PfANdE7_-6o.U-WccQ4KfCHnJDmEMl_lNHcoOMXVyObqxPZ-Pqutj1U";
            switch (familyId)
            {
                case Family.PE:
                    secret = "ISzo-4CbAC0.Hn_ShuiRjCrndLjZEOe2_8wF0Gi7rwwAwaMvkCdvdmY";
                    break;
                case Family.MSC:
                    secret = "PfANdE7_-6o.U-WccQ4KfCHnJDmEMl_lNHcoOMXVyObqxPZ-Pqutj1U";
                    break; 
                case Family.APM:
                    secret = "ISzo-4CbAC0.Hn_ShuiRjCrndLjZEOe2_8wF0Gi7rwwAwaMvkCdvdmY";
                    break;
                case Family.AOTH:
                    secret = "PfANdE7_-6o.U-WccQ4KfCHnJDmEMl_lNHcoOMXVyObqxPZ-Pqutj1U";
                    break;
            }
            var dbContext = DbContextFactory.GetDbContext();

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://directline.botframework.com/v3/directline/tokens/generate");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);
            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");

            var response = await client.SendAsync(request);
            string token = String.Empty;

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<DirectLineToken>(body).token;
            }
            var config = new ChatConfig()
            {
                Token = token,
                UserId = userId
            };
            return token;
        }

        [Route("SaveChatRecord")]
        [HttpPost]
        public bool SaveChatRecord([FromBody] ChatData request)
        {
            try
            {
                var dbContext = DbContextFactory.GetDbContext();
                Chat newChat = new Chat()
                {
                    IsQuestion = request.IsQuestion,
                    Content = request.Content,
                    TimeStamp = Convert.ToDateTime(request.TimeStamp),
                    Person = dbContext.People.Find(request.UserId)
                };
                dbContext.Chats.Add(newChat);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                Console.Write(e);
                return false;
            }
            return true;
        }

        [Route("PostQuestion")]
        [HttpPost]
        public bool PostQuestion([FromBody] QuestionData data)
        {
            var dbContext = DbContextFactory.GetDbContext();
            try
            {
                Question newQuestion = dbContext.Questions.Add(new Question()
                {
                    Title = data.Title == ""? "chat"+DateTime.Now.ToString(): data.Title,
                    Content = data.Title,
                    LikeNumber = 0,
                    CreateDate = DateTime.Now,
                    Product = dbContext.Products.First(),
                    Summary = "summary",
                    Supportor = dbContext.People.Find(data.UserId),
                    Person = dbContext.People.Find(data.UserId),
                    State = ""
                }) ;
                dbContext.SaveChanges();
                foreach(ChatData chat in data.Chats)
                {
                    dbContext.Comments.Add(new Comment()
                    {
                        Content = chat.Content,
                        CreateDate = Convert.ToDateTime(chat.TimeStamp),
                        Person = dbContext.People.Find(chat.UserId),
                        Question = dbContext.Questions.Find(newQuestion.Id),
                    });
                    dbContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public class QuestionData
         {
            public int UserId { get; set; }
            public string Title { get; set; }
            public ChatData[] Chats { get; set; }
        }

        public class ChatData
        {
            public bool IsQuestion { get; set; }
            public string TimeStamp { get; set; }
            public string Content { get; set; }
            public int UserId { get; set; }
        }

        public class DirectLineToken
        {
            public string conversationId { get; set; }
            public string token { get; set; }
            public int expires_in { get; set; }
        }
        public class ChatConfig
        {
            public string Token { get; set; }
            public int UserId { get; set; }
        }

        public enum Family{
            Unknow,
            PE,
            MSC,
            APM,
            AOTH
        }
    }
}
