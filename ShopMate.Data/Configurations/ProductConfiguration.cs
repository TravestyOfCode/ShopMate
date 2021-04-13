using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired(true);

            builder.Property(p => p.DefaultUnitSizeId)
                .IsRequired(true);

            builder.HasOne(p => p.DefaultUnitSize)
                .WithMany()
                .HasForeignKey(p => p.DefaultUnitSizeId)
                .HasConstraintName("FK_Product_UnitSize_DefaultUnitSizeId")
                .OnDelete(DeleteBehavior.ClientSetNull);                
        }
    }
}
