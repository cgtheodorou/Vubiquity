using System;
using System.Collections.Generic;
using System.Text;

namespace Vubiquity.Test.Data.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
