using Application.DTO;
using Application.DTO.Product;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Product;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ResultDTO CreateProduct(CreateProductDTO createProductDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrEmpty(createProductDTO.Name)
                || createProductDTO.Name.Length < 4
                || createProductDTO.Name.Length > 50)
            {
                resultDTO.Errors.Add("Product name cannot be empty and must be between 4 and 50 characters");
            }

            if (string.IsNullOrEmpty(createProductDTO.Description)
                || createProductDTO.Description.Length < 8
                || createProductDTO.Description.Length > 100)
            {
                resultDTO.Errors.Add("Product description cannot be empty and must be between 8 and 100 characters");
            }

            if (createProductDTO.CategoryId == Guid.Empty)
            {
                resultDTO.Errors.Add("Product category id cannot be empty");
            }

            if (createProductDTO.Price <= 0)
            {
                resultDTO.Errors.Add("Product price must be above 0");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.IsSuccess = false;
                return resultDTO;
            }

            var product = new Product(Guid.NewGuid(),
                createProductDTO.Name,
                createProductDTO.Description,
                createProductDTO.CategoryId,
                createProductDTO.Price);

            _productRepository.Add(product);
            resultDTO.IsSuccess = true;

            return resultDTO;
        }

        public ProductDTO? GetProduct(Guid id)
        {
            var product = _productRepository.Find(id);
            return _mapper.Map<ProductDTO?>(product);
        }

        public List<ProductDTO> GetProducts()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public List<ProductDTO> GetProductsByCategory(Guid categoryId)
        {
            var products = _productRepository.GetByCategory(categoryId);
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
