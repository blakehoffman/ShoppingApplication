﻿using AutoMapper;
using Dapper;
using Domain.Models.Product;
using Domain.Repositories;
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
        private readonly IMapper _mapper;

        public ProductRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Product product)
        {
            var storedProc = "InsertProduct";
            var productRecord = _mapper.Map<ProductRecord>(product);

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

            return _mapper.Map<Product?>(productRecord);
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

            return _mapper.Map<List<Product>>(productRecords);
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

            return _mapper.Map<List<Product>>(productRecords);
        }
    }
}
