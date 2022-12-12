using Application.DTO;
using Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ResultDTO> CreateProduct(CreateProductDTO createProductDTO);
        public Task<ProductDTO> GetProduct(Guid productId);
        public Task<List<ProductDTO>> GetProducts();
        public Task<List<ProductDTO>> GetProductsByCategory(Guid categoryId);
    }
}
