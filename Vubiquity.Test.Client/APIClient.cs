using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.Client
{
    public class APIClient
    {
        private readonly IRestClient _restClient;

        private readonly string _baseUri = "http://localhost:49245/api/v1/";
        private readonly string _basketResourceUri = "Basket/";
        private readonly string _basketItemResourceUri = "items";

        public APIClient(IClientConfiguration config = null)
        {
            if (config != null)
            {
                _baseUri = config?.BaseUri;
                _basketResourceUri = config?.BasketResourceUri;
                _basketItemResourceUri = config?.BasketItemResourceUri;
            }

            _restClient = new RestClient(_baseUri);
        }

        public List<BasketModel> GetAllBaskets()
        {
            var request = new RestRequest(_basketResourceUri, Method.GET);

            IRestResponse response = _restClient.Execute(request);
            var basketList = JsonConvert.DeserializeObject<List<BasketModel>>(response.Content);

            return basketList;
        }

        public BasketModel GetBasketById(string basketId)
        {
            var request = new RestRequest($"{_basketResourceUri}{basketId}", Method.GET);

            IRestResponse response = _restClient.Execute(request);
            var basket = JsonConvert.DeserializeObject<BasketModel>(response.Content);

            return basket;
        }

        public BasketModel GetBasketByUserId(string userId)
        {
            var request = new RestRequest($"{_basketResourceUri}user/{userId}", Method.GET);

            IRestResponse response = _restClient.Execute(request);
            var basket = JsonConvert.DeserializeObject<BasketModel>(response.Content);

            return basket;
        }

        public List<BasketModel> UpdateAllBasketAsync(string userId, IEnumerable<UpdateBasketRequestItemData> items)
        {
            var request = new RestRequest($"{_basketResourceUri}", Method.PUT, DataFormat.Json);

            request.AddJsonBody(new UpdateBasketRequest()
            {
                UserId = userId,
                Items = items
            });

            IRestResponse response = _restClient.Execute(request);
            var basketList = JsonConvert.DeserializeObject<List<BasketModel>>(response.Content);

            return basketList;
        }

        public BasketModel UpdateQuantitiesAsync(string basketId, ICollection<UpdateBasketItemData> itemsToUpdate)
        {
            var request = new RestRequest($"{_basketResourceUri}{_basketItemResourceUri}", Method.PUT, DataFormat.Json);

            request.AddJsonBody(new UpdateBasketItemsRequest()
            {
                BasketId = basketId,
                Updates = itemsToUpdate
            });

            IRestResponse response = _restClient.Execute(request);
            var updatedBasket = JsonConvert.DeserializeObject<BasketModel>(response.Content);

            return updatedBasket;
        }

        public bool AddBasketItemAsync(string basketId, string productId, int quantity)
        {
            var request = new RestRequest($"{_basketResourceUri}{_basketItemResourceUri}", Method.POST, DataFormat.Json);

            request.AddJsonBody(new AddBasketItemRequest()
            {
                BasketId = basketId,
                ProductId = productId,
                Quantity = quantity
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool AddBasketItemAsync(string basketId)
        {
            var request = new RestRequest($"{_basketResourceUri}{basketId}", Method.DELETE, DataFormat.Json);

            IRestResponse response = _restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
