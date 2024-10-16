using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class FamilyContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FamilyContract() { }
        public FamilyContract(Family family)
        {
            Id = family.Id;
            Name = family.Name;
        }
    }
}