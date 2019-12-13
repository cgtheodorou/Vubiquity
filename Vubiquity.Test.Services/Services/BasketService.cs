using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vubiquity.Test.Services.Interfaces;
using Vubiquity.Test.Data;
using Vubiquity.Test.Data.Models;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.Services
{
    public class BasketService : IBasketService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public BasketService(ApplicationDbContext context, IMapper mapper)
        {
            _applicationDbContext = context;
            _mapper = mapper;
        }

        public async Task<List<BasketModel>> GetAllAsync()
        {
            var baskets = await _applicationDbContext.Basket.Include(b => b.Items).ToListAsync();
            return _mapper.Map<List<BasketModel>>(baskets);
        }

        public async Task<BasketModel> GetByIdAsync(string id)
        {
            Guid.TryParse(id, out Guid basketId);

            var item = await _applicationDbContext.Basket.Include(b => b.Items).SingleOrDefaultAsync(b => b.Id == basketId);
            if (item != null)
            {
                var basket = _mapper.Map<BasketModel>(item);
                return basket;
            }

            return null;
        }

        public async Task<BasketModel> GetByUserIdAsync(string id)
        {
            Guid.TryParse(id, out Guid userId);

            var item = await _applicationDbContext.Basket.Include(b => b.Items).SingleOrDefaultAsync(b => b.UserId == userId);
            if (item != null)
            {
                var basket = _mapper.Map<BasketModel>(item);
                return basket;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(BasketModel basket)
        {
            Guid.TryParse(basket.UserId, out Guid userId);
            var item = _applicationDbContext.Basket.SingleOrDefault(b => b.UserId == userId);
            if (item != null)
            {
                var updatedBasket = _mapper.Map<Basket>(basket);
                _applicationDbContext.Update(updatedBasket);
            }
            else
            {
                var newBasket = _mapper.Map<Basket>(basket);
                _applicationDbContext.Basket.Add(newBasket);

            }

            var result = await _applicationDbContext.SaveChangesAsync();
            return result != 0;
        }

        public async Task<bool> DeleteAsync(BasketModel basket)
        {
            var basketEntity = _mapper.Map<Basket>(basket);
            _applicationDbContext.Basket.Remove(basketEntity);

            var result = await _applicationDbContext.SaveChangesAsync();
            return result != 0;
        }
    }
}
