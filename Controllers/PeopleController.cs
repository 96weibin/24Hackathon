using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static knowledgeBase.DataContract.PersonContract;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/People")]
    public class PeopleController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public PersonContract Login([FromBody] LoginData data)
        {
            var dbContext = DbContextFactory.GetDbContext();
            var person = dbContext.People.ToList().Where(x=>x.Name==data.Name&&x.Password==data.Password).FirstOrDefault();
            return new PersonContract(person);
        }

        public class LoginData
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}