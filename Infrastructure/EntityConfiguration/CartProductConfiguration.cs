using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(cartProduct => new { cartProduct.CartId, cartProduct.ProductId });

            builder
                .Property(cartProduct => cartProduct.ClusterId)
                .UseIdentityColumn();
        }
    }
}
