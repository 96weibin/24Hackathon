using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/AI")]
    public class QuestionController : ApiController
    {
        [Route("GetAllQuestionTypes")]
        [HttpGet]
        public List<QuestionTypeContract> GetAllQuestionTypes()
        {
            var dbContext = DbContextFactory.GetDbContext();
            return dbContext.Products.ToList().Select(q => new QuestionTypeContract(q)).ToList();
        }

        [Route("GetAllQuestionsCount")]
        [HttpGet]
        public int GetAllQuestionsCount() {
            var dbContext = DbContextFactory.GetDbContext();
            return dbContext.Questions.Count();
        }

        [Route("GetQuestions/{userId}/{page}/{size}/{state}/{order}")]
        [HttpGet]
        public List<QuestionContract> GetQuestions(int userId, int page,int size,int state,int order) {
            var dbContext = DbContextFactory.GetDbContext();
            var currentUser = dbContext.People.Find(userId);
            var dbQuestions = dbContext.Questions;
            if (order == (int)Sort.Descending)
            {
                switch (currentUser.RoleId)
                {
                    case (int)RoleType.RND:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser) && x.State == "Close").Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser) && x.State == "Open").Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser)).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }

                    case (int)RoleType.Customer:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Person == currentUser && x.State == "Close").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Person == currentUser && x.State == "Open").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Person == currentUser).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                    case (int)RoleType.Support:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Supportor == currentUser && x.State == "Close").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Supportor == currentUser && x.State == "Open").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderByDescending(x => x.CreateDate).ToList().Where(x => x.Supportor == currentUser).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                    default:
                        return null;
                }
            }
            else
            {
                switch (currentUser.RoleId)
                {
                    case (int)RoleType.RND:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser) && x.State == "Close").Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser) && x.State == "Open").Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.ReferPeople.Contains(currentUser)).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }

                    case (int)RoleType.Customer:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.Person == currentUser && x.State == "Close").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.Person == currentUser && x.State == "Open").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.Person == currentUser).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                    case (int)RoleType.Support:
                        if (state == (int)State.Close)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.Supportor == currentUser && x.State == "Close").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else if (state == (int)State.Open)
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).ToList().Where(x => x.Supportor == currentUser && x.State == "Open").Skip(size * page).Take(size).ToList().Select(x => new QuestionContract(x)).ToList();
                        }
                        else
                        {
                            return dbQuestions.OrderBy(x => x.CreateDate).Where(x => x.Supportor == currentUser).Skip(size * page).Take(size).Select(x => new QuestionContract(x)).ToList();
                        }
                    default:
                        return null;
                }
            }
           
        }

        [Route("AddOrUpdateQuestion")]
        [HttpPost]
        public QuestionContract AddOrUpdateQuestion([FromBody] AddOrUpdateQuestionRequest question)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var newQuestion = new Question();
            if (question.Id == null)
            {
                newQuestion = dbContext.Questions.Add(new Question()
                {
                    Title = question.Title,
                    Content = question.Content,
                    CreateDate = DateTime.Now,
                    Person = dbContext.People.Find(question.UserId),
                    Product = dbContext.Products.Find(question.ProductId),
                    Supportor=dbContext.People.Find(question.SupportId),
                });
            }
            else
            {
                newQuestion = dbContext.Questions.Find(question.Id);
                newQuestion.Title = question.Title;
                newQuestion.Content = question.Content;
            }
            dbContext.SaveChanges();
            return new QuestionContract(newQuestion);
        }

        [Route("DeleteQuestion/{questionId}")]
        [HttpGet]
        public bool DeleteQuestion(int questionId)
        {
            var dbContext = DbContextFactory.GetDbContext();
            string loginName = this.GetUsername();
            var questionDB = dbContext.Questions.Find(questionId);
            if (questionDB == null) return false;
            if (questionDB.Person.Name != loginName) return false;
            dbContext.Questions.Remove(questionDB);
            dbContext.SaveChanges();
            return true;
        }

        [Route("GetQuestionDetail/{questionId}")]
        [HttpGet]
        public QuestionContract GetQuestionDetail(int questionId)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var questionDB = dbContext.Questions.Find(questionId);
            return new QuestionContract(questionDB);
        }

        [Route("AddComment")]
        [HttpPost]
        public CommentContract AddComment([FromBody] AddCommentRequest comment)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var com = new Comment();
            com = dbContext.Comments.Add(new Comment()
            {
                Content = comment.Content,
                CreateDate = DateTime.Now,
                Person = dbContext.People.Find(comment.UserId),
                Question = dbContext.Questions.Find(comment.QuestionId),
                ParentCommentId = comment.ParentCommentId,
                IsRefer = comment.IsRefer
            });
            dbContext.SaveChanges();
            return new CommentContract(com);
        }

        [Route("UpdateComment")]
        [HttpPost]
        public CommentContract UpdateComment([FromBody] UpdateCommentRequest comment) {
            var dbContext = DbContextFactory.GetDbContext();
            var com = dbContext.Comments.Find(comment.Id);
            com.Content = comment.Content;
            dbContext.SaveChanges();
            return new CommentContract(com);
        }

        [Route("LikeComment/{commentId}/{personId}")]
        [HttpGet]
        public bool LikeComment(int commentId,int personId)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var com = dbContext.Comments.Find(commentId);
            var person = dbContext.People.Find(personId);
            if (com.PeopleDislike.Contains(person)) {
                com.PeopleDislike.Remove(person);
            }
            if (!com.PeopleLike.Contains(person)) {
                com.PeopleLike.Add(person);
            }
            dbContext.SaveChanges();
            return true;
        }

        [Route("DislikeComment/{commentId}/{personId}")]
        [HttpGet]
        public bool DislikeComment(int commentId, int personId)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var com = dbContext.Comments.Find(commentId);
            var person = dbContext.People.Find(personId);
            if (com.PeopleLike.Contains(person))
            {
                com.PeopleLike.Remove(person);
            }
            if (!com.PeopleDislike.Contains(person))
            {
                com.PeopleDislike.Add(person);
            }
            dbContext.SaveChanges();
            return true;
        }

        [Route("GetRNDPeople")]
        [HttpGet]
        public List<PersonContract> GetRNDPeople()
        {
            var dbContext = DbContextFactory.GetDbContext();
            return dbContext.People.Where(p => p.RoleId == (int)RoleType.RND).ToList().Select(p => new PersonContract(p)).ToList();
        }

        [Route("ReferPeople/{questionId}")]
        [HttpPost]
        public bool ReferPeople([FromBody] List<int> peopleIds, int questionId)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var question = dbContext.Questions.Find(questionId);
            var people = dbContext.People.Where(x => peopleIds.Contains(x.Id)).ToList();
            people.ForEach(x=> question.ReferPeople.Add(x));
            dbContext.SaveChanges();
            return true;
        }

        [Route("UpdateQuestionState/{questionId}")]
        [HttpGet]
        public bool UpdateQuestionState(int questionId) {
            var dbContext = DbContextFactory.GetDbContext();
            var question = dbContext.Questions.Find(questionId);
            question.State = "Close";
            dbContext.SaveChanges();
            return true;
        }

        [Route("AddSummaryToQuestion/{questionId}")]
        [HttpPost]
        public QuestionContract AddSummaryToQuestion([FromBody] string summary, int questionId) {
            var dbContext = DbContextFactory.GetDbContext();
            var question = dbContext.Questions.Find(questionId);
            question.State = "Close";
            question.Summary = summary;
            dbContext.SaveChanges();
            return new QuestionContract(question);
        }

        [Route("FilterQuestionByProduct/{ProductId}")]
        [HttpPost]
        public List<QuestionContract> FilterQuestionByProduct(int ProductId) {
            var dbContext = DbContextFactory.GetDbContext();
            return dbContext.Questions.ToList().Where(x=>x.Product.Id == ProductId).Select(x=>new QuestionContract(x)).ToList();
        }
    }

   public class AddOrUpdateQuestionRequest {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? ProductId { get; set; }
        public int? SupportId { get; set; }
    }

    public class AddCommentRequest {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Content { get; set; }
        public bool IsRefer { get; set; }
    }

    public class UpdateCommentRequest {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public enum State{
        All=0,
        Open=1,
        Close=2
    }

    public enum Sort {
        Ascending=0,
        Descending=1,
    }

}


