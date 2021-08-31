using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public Product? Find(Guid id);
        public Product? FindByName(string name);
        public List<Product> GetAll();
        public List<Product> GetByCategory(Guid categoryId);
    }
}
