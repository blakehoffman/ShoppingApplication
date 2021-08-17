using AutoMapper;
using Dapper;
using Domain.Models.Categories;
using Domain.Repositories;
using Infrastructure.Records;
using System.Data.SqlClient;

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
                connection.Execute(storedProc, discountRecord);
            }
        }

        public List<Category> GetAll(Guid parentId)
        {
            var storedProc = "GetCategories";
            List<CategoryRecord> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = connection.Query<CategoryRecord>(storedProc, parentId).ToList();
            }

            return _mapper.Map<List<Category>>(result);
        }
    }
}
