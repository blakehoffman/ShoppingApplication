using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(orderProduct => new { orderProduct.OrderId, orderProduct.ProductId });
            
            builder
                .Property(orderProduct => orderProduct.ClusterId)
                .UseIdentityColumn();
        }
    }
}
