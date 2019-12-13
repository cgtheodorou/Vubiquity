using System;
using System.Collections.Generic;
using Vubiquity.Test.Client;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.ConsoleClient
{
    /// <summary>
    /// Just a test console using the client
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var client = new APIClient();

            var result = client.GetAllBaskets();

            var basket = client.GetBasketById(result[0].Id);

            basket = client.GetBasketByUserId(basket.UserId);

            var updateRequestItem = new UpdateBasketRequestItemData()
            {
                BasketId = basket.Id,
                ProductId = basket.Items[0].ProductId,
                Quantity = 5
            };

            var updateRequestItemList = new List<UpdateBasketRequestItemData>
            {
                updateRequestItem
            };

            result = client.UpdateAllBasketAsync(result[0].UserId, updateRequestItemList);

            var updateRequestQuantitiesItemList = new List<UpdateBasketItemData>()
            {
                new UpdateBasketItemData()
                {
                     BasketItemId = result[0].Items[0].Id,
                     NewQty = 10
                }
            };

            basket = client.UpdateQuantitiesAsync(result[0].Id, updateRequestQuantitiesItemList);

            Console.ReadKey();
        }
    }
}
