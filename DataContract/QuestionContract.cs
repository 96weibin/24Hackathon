using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class QuestionContract
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? LikeNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public  string PersonName { get; set; }
        public  List<CommentContract> Comments { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public List<PersonContract> ReferPeople { get; set; }
        public string Summary { get; set; }
        public string State { get; set; }
        public int SupportorId { get; set; }
        public string FamilyName { get; set; }
        public QuestionContract()
        {
            Comments = new List<CommentContract>();
        }
        public QuestionContract(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            LikeNumber = question.LikeNumber;
            CreateDate = question.CreateDate;
            PersonName = question.Person.Name;
            Comments = question.Comments.Select(c=>new CommentContract(c)).ToList();
            QuestionTypeId = question.Product.Id;
            QuestionType = question.Product.Name;
            ReferPeople = question.ReferPeople.Select(x=>new PersonContract(x)).ToList();
            Summary = question.Summary;
            State = question.State;
            SupportorId = question.Supportor.Id;
            FamilyName = question.Product.Family.Name;
        }
    }
}