using System;

namespace Mango.Services.ProductAPI.Dtos
{
    public class CreateOrUpdateProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
    
    public class ProductDto : CreateOrUpdateProductDto
    {
        public Guid Id { get; set; }
    }
}