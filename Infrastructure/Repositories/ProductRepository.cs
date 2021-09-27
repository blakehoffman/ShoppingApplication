using AutoMapper;
using Dapper;
using Domain.Models.Product;
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
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Product product)
        {
            var storedProc = "InsertProduct";
            var productRecord = ProductMapper.MapToProductRecord(product);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc,
                    productRecord,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Product? Find(Guid id)
        {
            var storedProc = "GetProduct";
            ProductRecord? productRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                productRecord = connection.Query<ProductRecord?>(storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return ProductMapper.MapToProduct(productRecord);
        }

        public Product? FindByName(string name)
        {
            var storedProc = "FindProductByName";
            ProductRecord? productRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                productRecord = connection.Query<ProductRecord?>(storedProc,
                    new { name },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return ProductMapper.MapToProduct(productRecord);
        }

        public List<Product> GetAll()
        {
            var storedProc = "GetProducts";
            List<ProductRecord> productRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                productRecords = connection.Query<ProductRecord>(storedProc,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            var products = new List<Product>();

            foreach (var productRecord in productRecords)
            {
                products.Add(ProductMapper.MapToProduct(productRecord));
            }

            return products;
        }

        public List<Product> GetByCategory(Guid categoryId)
        {
            var storedProc = "GetProductsByCategory";
            List<ProductRecord> productRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                productRecords = connection.Query<ProductRecord>(storedProc,
                    new { categoryId },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            var products = new List<Product>();

            foreach (var productRecord in productRecords)
            {
                products.Add(ProductMapper.MapToProduct(productRecord));
            }

            return products;
        }
    }
}
