using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(order => order.Discount)
                .WithMany()
                .HasForeignKey("DiscountId");

            builder
                .Property(order => order.ClusterId)
                .UseIdentityColumn();
        }
    }
}
