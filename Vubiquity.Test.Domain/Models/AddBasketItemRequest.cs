using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vubiquity.Test.Domain.Models
{
    public class AddBasketItemRequest
    {
        public string ProductId { get; set; }
        public string BasketId { get; set; }

        public int Quantity { get; set; }

        public AddBasketItemRequest()
        {
            Quantity = 1;
        }
    }
}
