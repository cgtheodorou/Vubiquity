using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vubiquity.Test.Domain.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
