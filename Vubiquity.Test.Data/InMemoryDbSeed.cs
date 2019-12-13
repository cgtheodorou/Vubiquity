using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vubiquity.Test.Data.Models;

namespace Vubiquity.Test.Data
{
    public class InMemoryDbSeed
    {
        const string TestBasketId = "c41fd3c6-0461-4359-b18f-081ba71b4b4d";
        const string TestBasketItemId = "045aab6d-e9ed-4cdf-9616-e6c8598a86ac";
        const string TestProductId = "88859bd7-40e3-487c-bba7-57afb53e4c4c";
        const string TestUserId = "cf63beb5-0548-47c6-aa52-88cecd5f1bb2";

        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Basket.Any())
            {
                var basketItems = new List<Models.BasketItem>()
                    {
                        new Models.BasketItem
                        {
                            Id = Guid.Parse(TestBasketItemId),
                            ProductId = Guid.Parse(TestProductId),
                            ProductName = "Test Product",
                            ProductImageUrl = "productimg.jpg",
                            Quantity = 4,
                            UnitPrice = 20
                        }
                    };

                context.Basket.AddRange(
                    new Models.Basket
                    {
                        Id = Guid.Parse(TestBasketId),
                        UserId = Guid.Parse(TestUserId),
                        Items = basketItems
                    });
            }

            if (!context.Product.Any())
            {
                context.Product.AddRange(
                    new Product()
                    {
                        Id = Guid.Parse(TestProductId),
                        Name = "Test Product",
                        ImageUrl = "productimg.jpg",
                        Price = 20
                    }
                );
            }

            context.SaveChanges();
        }
    }

}
