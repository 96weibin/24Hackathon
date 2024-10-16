using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class PersonContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public string Password { get; set; }
      
        public PersonContract(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Password = person.Password;
            Role = (RoleType)person.RoleId;
        }
    }
    
    
}
public enum RoleType
{
    RND=1,
    Support=2,
    Customer=3
}