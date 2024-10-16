using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class QuestionTypeContract
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int SupportorId { get; set; }
        public FamilyContract Family { get; set; }
        public QuestionTypeContract(Product product)
        {
            Id = product.Id;
            Type = product.Name;
            //SupportorId = product.Supportor.Id;
            Family = new FamilyContract(product.Family);
        }
    }
}