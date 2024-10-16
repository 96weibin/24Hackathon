using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static knowledgeBase.DataContract.FamilyContract;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/Family")]
    public class FamilyController : ApiController
    {
        [Route("GetFamilyList")]
        [HttpGet]
        public List<FamilyContract> GetFamilyList()
        {
            var dbContext = DbContextFactory.GetDbContext();
            var families = dbContext.Families.ToList();
            var familyData = families.Select(g => new FamilyContract(g)).ToList();
            return familyData;
        }

    }
}