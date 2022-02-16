using Dapper;
using Domain.Models.Categories;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Mappings;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Category category)
        {
            var storedProc = "InsertCategory";
            var discountRecord = CategoryMapper.MapToCategoryRecord(category);

            _unitOfWork.Connection.Execute(
                storedProc,
                discountRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);
        }

        public Category Find(Guid id)
        {
            var storedProc = "GetCategory";
            CategoryRecord categoryRecord;

            categoryRecord = _unitOfWork.Connection.Query<CategoryRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return CategoryMapper.MapToCategory(categoryRecord);
        }

        public Category FindByName(string name)
        {
            var storedProc = "GetCategoryByName";
            CategoryRecord categoryRecord;

            categoryRecord = _unitOfWork.Connection.Query<CategoryRecord>(
                storedProc,
                new { name },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return CategoryMapper.MapToCategory(categoryRecord);
        }

        public List<Category> GetAll(Guid? parentId)
        {
            var storedProc = "GetCategories";
            List<CategoryRecord> categoryRecords;

            categoryRecords = _unitOfWork.Connection.Query<CategoryRecord>(
                storedProc,
                new { parentId },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            var categories = new List<Category>();

            foreach (var categoryRecord in categoryRecords)
            {
                categories.Add(CategoryMapper.MapToCategory(categoryRecord));
            }

            return categories;
        }
    }
}
