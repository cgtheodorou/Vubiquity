using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vubiquity.Test.Domain.Models
{
    public class BasketModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItemModel> Items { get; set; }

        public BasketModel()
        {

        }

        public BasketModel(string userId)
        {
            UserId = userId;
            Items = new List<BasketItemModel>();
        }
    }
}
