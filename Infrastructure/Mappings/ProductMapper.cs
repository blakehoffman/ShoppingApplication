using Domain.Models.Product;
using Infrastructure.Records;

namespace Infrastructure.Mappings
{
    public static class ProductMapper
    {

        public static Product MapToProduct(ProductRecord productRecord)
        {
            if (productRecord == null)
            {
                return null;
            }

            return new Product(
                productRecord.Id,
                productRecord.Name,
                productRecord.Description,
                productRecord.CategoryId,
                productRecord.Price);
        }

        public static ProductRecord MapToProductRecord(Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductRecord
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price
            };
        }
    }
}
