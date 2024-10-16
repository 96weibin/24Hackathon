using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace knowledgeBase.DataContract
{
    public class ChatData
    {
        public int Id { get; set; }
        public bool IsQuestion { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }
        public PersonContract User { get; set; }
        public ChatData() { }
        public ChatData(Chat chatData)
        {
            IsQuestion = chatData.IsQuestion;
            TimeStamp = chatData.TimeStamp;
            User = new PersonContract(chatData.Person);
            Content = chatData.Content;
        }
    }
}


