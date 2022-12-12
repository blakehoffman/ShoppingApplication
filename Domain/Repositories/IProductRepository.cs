using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        public Task Add(Product product);
        public Task<Product> Find(Guid id);
        public Task<Product> FindByName(string name);
        public Task<List<Product>> GetAll();
        public Task<List<Product>> GetByCategory(Guid categoryId);
    }
}
