using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public class UnitSizeConfiguration : IEntityTypeConfiguration<UnitSize>
    {
        public void Configure(EntityTypeBuilder<UnitSize> builder)
        {
            builder.ToTable(nameof(UnitSize));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired(true);

            builder.HasIndex(p => p.Name)
                .IsUnique(true)
                .HasDatabaseName("AK_UnitSize_Name");
        }
    }
}
