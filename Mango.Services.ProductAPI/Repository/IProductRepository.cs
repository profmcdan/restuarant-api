using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mango.Services.ProductAPI.Dtos;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(Guid productId);
        Task<ProductDto> CreateProduct(CreateOrUpdateProductDto productDto);
        Task<ProductDto> UpdateProduct(Guid productId, CreateOrUpdateProductDto productDto);
        Task<bool> DeleteProduct(Guid productId);
    }
}