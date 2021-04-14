using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public class ProductConfiguration : EntityBaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

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

            builder.HasIndex(p => p.Name)
                .IsUnique(true)
                .IsClustered(false)
                .HasDatabaseName("UX_Product_Name");
        }
    }
}
