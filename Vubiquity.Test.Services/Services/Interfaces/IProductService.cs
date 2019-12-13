using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<List<ProductModel>> GetProductsAsync(IEnumerable<string> items);
        Task<ProductModel> GetProductByIdAsync(string id);
    }
}
