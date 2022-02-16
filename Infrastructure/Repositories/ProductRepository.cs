using Dapper;
using Domain.Models.Product;
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
    public class ProductRepository : IProductRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Product product)
        {
            var storedProc = "InsertProduct";
            var productRecord = ProductMapper.MapToProductRecord(product);

            _unitOfWork.Connection.Execute(
                storedProc,
                productRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);
        }

        public Product Find(Guid id)
        {
            var storedProc = "GetProduct";
            ProductRecord productRecord;

            productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return ProductMapper.MapToProduct(productRecord);
        }

        public Product FindByName(string name)
        {
            var storedProc = "FindProductByName";
            ProductRecord productRecord;

            productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                storedProc,
                new { name },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return ProductMapper.MapToProduct(productRecord);
        }

        public List<Product> GetAll()
        {
            var storedProc = "GetProducts";
            List<ProductRecord> productRecords;

            productRecords = _unitOfWork.Connection.Query<ProductRecord>(
                storedProc,
                transaction: _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

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

            productRecords = _unitOfWork.Connection.Query<ProductRecord>(
                storedProc,
                new { categoryId },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            var products = new List<Product>();

            foreach (var productRecord in productRecords)
            {
                products.Add(ProductMapper.MapToProduct(productRecord));
            }

            return products;
        }
    }
}
