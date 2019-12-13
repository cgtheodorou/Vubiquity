using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vubiquity.Test.Domain.Models
{
    public class UpdateBasketRequest
    {
        public string UserId { get; set; }

        public IEnumerable<UpdateBasketRequestItemData> Items { get; set; }
    }

    public class UpdateBasketRequestItemData
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
