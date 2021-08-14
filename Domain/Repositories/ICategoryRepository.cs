using Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public Category FindChildCategory(Guid id);
        public List<Category> GetAll();
    }
}
