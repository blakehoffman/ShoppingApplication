using Domain.Models.Categories;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public Category Find(Guid id);
        public Category FindByName(string name);
        public List<Category> GetAll(Guid? parentId);
    }
}
