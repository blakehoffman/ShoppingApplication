using Domain.Models.Product;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Product product)
        {
            var productEntity = ProductMapper.MapToProductEntity(product);
            _applicationDbContext.Products.Add(productEntity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Product> Find(Guid id)
        {
            var productEntity = await _applicationDbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            return ProductMapper.MapToProduct(productEntity);
        }

        public async Task<Product> FindByName(string name)
        {
            var productEntity = await _applicationDbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Name == name);

            return ProductMapper.MapToProduct(productEntity);
        }

        public async Task<List<Product>> GetAll()
        {
            var productEntities = await _applicationDbContext.Products
                .AsNoTracking()
                .ToListAsync();

            var products = new List<Product>();

            foreach (var productEntity in productEntities)
            {
                products.Add(ProductMapper.MapToProduct(productEntity));
            }
            return products;
        }

        public async Task<List<Product>> GetByCategory(Guid categoryId)
        {
            var productEntities = await _applicationDbContext.Products
                .Where(product => product.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();

            var products = new List<Product>();

            foreach (var productEntity in productEntities)
            {
                products.Add(ProductMapper.MapToProduct(productEntity));
            }
            return products;
        }
    }
}
