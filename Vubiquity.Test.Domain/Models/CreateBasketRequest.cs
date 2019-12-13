using System;
using System.Collections.Generic;

namespace Vubiquity.Test.Domain.Models
{
    public class CreateBasketRequest
    {
        public List<BasketItemModel> Items { get; set; }
    }
}   
