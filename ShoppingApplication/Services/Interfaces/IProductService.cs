using Application.DTO;
using Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        public ResultDTO CreateProduct(CreateProductDTO createProductDTO);
        public ProductDTO? GetProduct(Guid productId);
        public List<ProductDTO> GetProducts();
        public List<ProductDTO> GetProductsByCategory(Guid categoryId);
    }
}
