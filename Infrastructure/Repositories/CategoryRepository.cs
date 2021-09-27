using AutoMapper;
using Dapper;
using Domain.Models.Categories;
using Domain.Repositories;
using Infrastructure.Mappings;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Category category)
        {
            var storedProc = "InsertCategory";
            var discountRecord = CategoryMapper.MapToCategoryRecord(category);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    storedProc,
                    discountRecord,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Category? Find(Guid id)
        {
            var storedProc = "GetCategory";
            CategoryRecord? categoryRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                categoryRecord = connection.Query<CategoryRecord?>(
                    storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return CategoryMapper.MapToCategory(categoryRecord);
        }

        public Category? FindByName(string name)
        {
            var storedProc = "GetCategoryByName";
            CategoryRecord? categoryRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                categoryRecord = connection.Query<CategoryRecord?>(
                    storedProc,
                    new { name },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return CategoryMapper.MapToCategory(categoryRecord);
        }

        public List<Category> GetAll(Guid? parentId)
        {
            var storedProc = "GetCategories";
            List<CategoryRecord> categoryRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                categoryRecords = connection.Query<CategoryRecord>(
                    storedProc,
                    new { parentId },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            var categories = new List<Category>();

            foreach (var categoryRecord in categoryRecords)
            {
                categories.Add(CategoryMapper.MapToCategory(categoryRecord));
            }

            return categories;
        }
    }
}
