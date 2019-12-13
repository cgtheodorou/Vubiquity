using System;
using System.Collections.Generic;
using System.Text;

namespace Vubiquity.Test.Data.Models
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
