using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mango.Services.ProductAPI.DbContext;
using Mango.Services.ProductAPI.Dtos;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            Product product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProduct(CreateOrUpdateProductDto productDto)
        {
            var product = _mapper.Map<CreateOrUpdateProductDto, Product>(productDto);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }
        
        public async Task<ProductDto> UpdateProduct(Guid productId, CreateOrUpdateProductDto productDto)
        {
            // [TODO]: Review this implementation
            Product productToUpdate = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            var product = _mapper.Map<CreateOrUpdateProductDto, Product>(productDto);
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                Product product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}