using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class ProductContract
    {
        public class ProductData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int FamilyId { get; set; }
            public ProductData() { }
            public ProductData(Product product)
            {
                Id = product.Id;
                Name = product.Name;
                FamilyId = product.FamilyId;
            }
        }
    }
}