using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vubiquity.Test.BasketAPI.Controllers;
using Vubiquity.Test.Services.Interfaces;
using Vubiquity.Test.Domain.Models;
using Xunit;

namespace Vubiquity.Test.BasketAPI.Tests
{
    public class Basket
    {
        [Fact]
        public async Task GetAllBaskets_ReturnsBaskets_Success()
        {
            //Arrange
            var mockedBasketService = Substitute.For<IBasketService>();
            mockedBasketService.GetAllAsync().Returns(new List<BasketModel>() {
                 new BasketModel()
                 {
                     Id = "c41fd3c6-0461-4359-b18f-081ba71b4b4d",
                     UserId = "c41fd3c6-0461-4359-b18f-081ba71b4b4d",
                     Items = new List<BasketItemModel>()
                 }
            });

            //Act
            var ctrl = new BasketController(mockedBasketService, null);
            var baskets = await ctrl.GetAllBasketsAsync();

            //Assert
            Assert.NotNull(baskets);
            Assert.True(baskets.Value.Count > 0);
            Assert.True(baskets.Value[0].Items.Count == 0);
        }

        [Fact]
        public async Task GetBasketById_ReturnsBasket_Success()
        {
            //Arrange
            var mockedBasketService = Substitute.For<IBasketService>();
            mockedBasketService.GetByIdAsync("c41fd3c6-0461-4359-b18f-081ba71b4b4d").Returns(new BasketModel() {
                 Id = "c41fd3c6-0461-4359-b18f-081ba71b4b4d"
            });

            //Act
            var ctrl = new BasketController(mockedBasketService, null);
            var response = await ctrl.GetBasketAsync("c41fd3c6-0461-4359-b18f-081ba71b4b4d");

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Value.Id == "c41fd3c6-0461-4359-b18f-081ba71b4b4d");
        }

        [Fact]
        public async Task GetBasketById_ReturnsBadRequest()
        {
            //Arrange
            var mockedBasketService = Substitute.For<IBasketService>();
            mockedBasketService.GetByIdAsync("").Returns(new BasketModel());

            //Act
            var ctrl = new BasketController(mockedBasketService, null);
            var response = await ctrl.GetBasketAsync("");

            //Assert
            Assert.NotNull(response);
            Assert.True((response.Result as BadRequestObjectResult).StatusCode == 400);
        }


        [Fact]
        public async Task GetBasketByUserId_ReturnsBasket_Success()
        {
            //Arrange
            var mockedBasketService = Substitute.For<IBasketService>();
            mockedBasketService.GetByUserIdAsync("cf63beb5-0548-47c6-aa52-88cecd5f1bb2").Returns(new BasketModel() {
                UserId = "cf63beb5-0548-47c6-aa52-88cecd5f1bb2"
            });

            //Act
            var ctrl = new BasketController(mockedBasketService, null);
            var response = await ctrl.GetBasketByUserId("cf63beb5-0548-47c6-aa52-88cecd5f1bb2");

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Value.UserId == "cf63beb5-0548-47c6-aa52-88cecd5f1bb2");
        }

        [Fact]
        public async Task GetBasketByUserId_ReturnsBadRequest()
        {
            //Arrange
            var mockedBasketService = Substitute.For<IBasketService>();
            mockedBasketService.GetByIdAsync("").Returns((BasketModel)null);

            //Act
            var ctrl = new BasketController(mockedBasketService, null);
            var response = await ctrl.GetBasketByUserId("");

            //Assert
            Assert.NotNull(response);
            Assert.True((response.Result as BadRequestObjectResult).StatusCode == 400);
        }
    }
}
