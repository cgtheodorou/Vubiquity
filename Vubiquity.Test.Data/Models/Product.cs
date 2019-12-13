using System;
using System.Collections.Generic;
using System.Text;

namespace Vubiquity.Test.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
