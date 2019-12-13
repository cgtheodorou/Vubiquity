using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vubiquity.Test.Services.Interfaces;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.BasketAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService, IProductService productService)
        {
            _productService = productService;
            _basketService = basketService;
        }

        /// <summary>
        /// Retrieves all baskets in memorydb
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BasketModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BasketModel>>> GetAllBasketsAsync()
        {
            return await _basketService.GetAllAsync();
        }

        /// <summary>
        /// Retrieves basket by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketModel>> GetBasketAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid id");

            var basket = await _basketService.GetByIdAsync(id);

            if (basket == null)
                return BadRequest("Basket doesn't exist");

            return basket;
        }

        /// <summary>
        /// Retrieves basket by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("user/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketModel>> GetBasketByUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid id");

            var basket = await _basketService.GetByUserIdAsync(id);

            if (basket == null)
                return BadRequest("Basket doesn't exist");

            return basket;
        }

        /// <summary>
        /// Create/Update basket with items
        /// </summary>
        /// <param name="basketRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateAllBasketAsync([FromBody] UpdateBasketRequest basketRequest)
        {
            if (basketRequest.Items == null || !basketRequest.Items.Any())
            {
                return BadRequest("Need to provide at least one basket item");
            }

            var basket = await _basketService.GetByUserIdAsync(basketRequest.UserId) ?? new BasketModel(basketRequest.UserId);

            var productItems = await _productService.GetProductsAsync(basketRequest.Items.Select(x => x.ProductId));

            foreach (var item in basketRequest.Items)
            {
                var productItem = productItems.SingleOrDefault(p => p.Id == item.ProductId);
                if (productItem == null)
                {
                    return BadRequest($"({item.ProductId}) does not exist.");
                }

                basket.Items.Add(new BasketItemModel()
                {
                    Id = item.BasketId,
                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                    ProductImageUrl = productItem.ImageUrl,
                    UnitPrice = productItem.Price,
                    Quantity = item.Quantity
                });
            }

            var result = await _basketService.UpdateAsync(basket);

            return result;
        }

        /// <summary>
        /// Updates quantities of items in basket
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketModel>> UpdateQuantitiesAsync([FromBody] UpdateBasketItemsRequest data)
        {
            if (!data.Updates.Any())
            {
                return BadRequest("No updates sent");
            }

            var currentBasket = await _basketService.GetByIdAsync(data.BasketId);
            if (currentBasket == null)
            {
                return BadRequest($"Basket with id {data.BasketId} not found.");
            }

            foreach (var update in data.Updates)
            {
                var basketItem = currentBasket.Items.SingleOrDefault(bitem => bitem.Id == update.BasketItemId);
                if (basketItem == null)
                {
                    return BadRequest($"Basket item with id {update.BasketItemId} not found");
                }
                basketItem.Quantity = update.NewQty;
            }

            await _basketService.UpdateAsync(currentBasket);

            return currentBasket;
        }

        /// <summary>
        /// Adds item to basket
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddBasketItemAsync([FromBody] AddBasketItemRequest data)
        {
            if (data == null || data.Quantity == 0)
            {
                return BadRequest("Invalid data");
            }

            var item = await _productService.GetProductByIdAsync(data.ProductId);

            var currentBasket = (await _basketService.GetByIdAsync(data.BasketId)) ?? new BasketModel(data.BasketId);

            currentBasket.Items.Add(new BasketItemModel()
            {
                UnitPrice = item.Price,
                ProductImageUrl = item.ImageUrl,
                ProductId = item.Id,
                ProductName = item.Name,
                Quantity = data.Quantity
            });

            await _basketService.UpdateAsync(currentBasket);

            return Ok();
        }

        /// <summary>
        /// Deletes basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid basket id");

            var currentBasket = await _basketService.GetByIdAsync(id);

            if (currentBasket == null)
                return BadRequest("No such basket exists");

            await _basketService.DeleteAsync(currentBasket);

            return Ok();
        }
    }
}
