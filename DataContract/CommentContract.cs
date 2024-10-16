using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class CommentContract
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? LikeNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public int QuestionId { get; set; }
        public int? ParentCommentId { get; set; }
        public string PersonName  { get; set; }
        public bool IsRefer { get; set; }
        public bool IsLikeComment { get; set; }
        public bool IsDislikeComment { get; set; }
        public CommentContract(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            LikeNumber = comment.LikeNumber;
            CreateDate = comment.CreateDate;
            QuestionId = comment.Question.Id;
            ParentCommentId = comment.ParentCommentId;
            PersonName = comment.Person.Name;
            IsRefer = comment.IsRefer;
            IsLikeComment = comment.PeopleLike.Contains(comment.Question.Person);
            IsDislikeComment = comment.PeopleDislike.Contains(comment.Question.Person);
        }
    }
}