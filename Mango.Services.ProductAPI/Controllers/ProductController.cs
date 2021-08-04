using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mango.Services.ProductAPI.Dtos;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        protected ResponseDto _response;

        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetProducts()
        {
            try
            {
                IEnumerable<ProductDto> products = await _productRepository.GetProducts();
                _response.Result = products;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }

            return _response;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetProductById(Guid id)
        {
            try
            {
                ProductDto product = await _productRepository.GetProductById(id);
                _response.Result = product;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }

            return Ok(_response);
        }
        
        [HttpPost]
        public async Task<object> Post([FromBody] CreateOrUpdateProductDto newProduct)
        {
            try
            {
                ProductDto product = await _productRepository.CreateProduct(newProduct);
                _response.Result = product;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }

            return Ok(_response);
        }
        
        [HttpPut]
        public async Task<object> UpdateProduct([FromBody] CreateOrUpdateProductDto product, Guid productId)
        {
            try
            {
                var productToUpdate = await _productRepository.GetProductById(productId);
                if (productToUpdate == null)
                {
                    return NotFound();
                }
                ProductDto updatedProduct = await _productRepository.UpdateProduct(productId, productToUpdate);
                _response.Result = updatedProduct;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }

            return _response;
        }
        
        [HttpDelete]
        public async Task<object> DeleteProduct(Guid productId)
        {
            try
            {
                var isDeleted = await _productRepository.DeleteProduct(productId);
                
                // ReSharper disable once HeapView.BoxingAllocation
                _response.Result = isDeleted;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }

            return Ok(_response);
        }
    }
}
