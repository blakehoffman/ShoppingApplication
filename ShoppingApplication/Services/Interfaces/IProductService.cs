using Application.DTO;
using Application.DTO.Product;
using System;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        public ResultDTO CreateProduct(CreateProductDTO createProductDTO);
        public ProductDTO GetProduct(Guid productId);
        public List<ProductDTO> GetProducts();
        public List<ProductDTO> GetProductsByCategory(Guid categoryId);
    }
}
