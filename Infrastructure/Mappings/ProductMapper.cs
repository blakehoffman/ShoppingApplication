using Domain.Models.Product;
using Infrastructure.Records;
using EntityModels = Infrastructure.Models;

namespace Infrastructure.Mappings
{
    public static class ProductMapper
    {

        public static Product MapToProduct(EntityModels.Product productEntity)
        {
            if (productEntity == null)
            {
                return null;
            }

            return new Product(
                productEntity.Id,
                productEntity.Name,
                productEntity.Description,
                productEntity.CategoryId,
                productEntity.Price);
        }

        public static EntityModels.Product MapToProductEntity(Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new EntityModels.Product
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
