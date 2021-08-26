using AutoMapper;
using Dapper;
using Domain.Models.Categories;
using Domain.Repositories;
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
        private readonly IMapper _mapper;

        public CategoryRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Category category)
        {
            var storedProc = "InsertCategory";
            var discountRecord = _mapper.Map<CategoryRecord>(category);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc,
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
                categoryRecord = connection.Query<CategoryRecord>(storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return _mapper.Map<Category?>(categoryRecord);
        }

        public Category? FindByName(string name)
        {
            var storedProc = "GetCategoryByName";
            CategoryRecord? categoryRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                categoryRecord = connection.Query<CategoryRecord>(storedProc,
                    new { name },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return _mapper.Map<Category?>(categoryRecord);
        }

        public List<Category> GetAll(Guid? parentId)
        {
            var storedProc = "GetCategories";
            List<CategoryRecord> categoryRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                categoryRecords = connection.Query<CategoryRecord>(storedProc,
                    new { parentId },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            return _mapper.Map<List<Category>>(categoryRecords);
        }
    }
}
