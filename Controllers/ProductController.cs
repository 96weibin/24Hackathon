using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static knowledgeBase.DataContract.ProductContract;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [Route("GetProductList")]
        [HttpGet]
        public List<ProductData> GetProductList()
        {
            var dbContext = DbContextFactory.GetDbContext();
            var products = dbContext.Products.ToList();
            var productData = products.Select(g => new ProductData(g)).ToList();
            return productData;
        }

    }
}