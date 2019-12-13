using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketModel> GetByUserIdAsync(string userId);
        Task<bool> UpdateAsync(BasketModel basket);
        Task<BasketModel> GetByIdAsync(string id);
        Task<List<BasketModel>> GetAllAsync();
        Task<bool> DeleteAsync(BasketModel basket);
    }
}
