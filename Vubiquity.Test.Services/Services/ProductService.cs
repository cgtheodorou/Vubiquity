using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vubiquity.Test.Data;
using Vubiquity.Test.Domain.Models;
using Vubiquity.Test.Services.Interfaces;

namespace Vubiquity.Test.BasketAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _applicationDbContext = context;
            _mapper = mapper;
        }

        public async Task<ProductModel> GetProductByIdAsync(string id)
        {
            Guid.TryParse(id, out Guid productId);

            var product = await _applicationDbContext.Product.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                var productModel = _mapper.Map<ProductModel>(product);
                return productModel;
            }

            return null;
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var products = await _applicationDbContext.Product.ToListAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return productModels;
        }

        public async Task<List<ProductModel>> GetProductsAsync(IEnumerable<string> items)
        {
            var products = await _applicationDbContext.Product
                .Where(p => items.Contains(p.Id.ToString()))
                .ToListAsync();

            var productModels = _mapper.Map<List<ProductModel>>(products);
            return productModels;
        }
    }
}
