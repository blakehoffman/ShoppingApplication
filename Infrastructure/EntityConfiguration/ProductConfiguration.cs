using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(product => product.Category)
                .WithMany()
                .HasForeignKey("CategoryId");

            builder
                .Property(product => product.ClusterId)
                .UseIdentityColumn();
        }
    }
}
