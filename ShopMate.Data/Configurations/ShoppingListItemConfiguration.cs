using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public class ShoppingListItemConfiguration : EntityBaseConfiguration<ShoppingListItem>
    {
        public override void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(ShoppingListItem));

            builder.HasKey(p => new { p.ShoppingListId, p.ProductId });

            builder.Property(p => p.Quantity)
                .HasPrecision(9, 5)
                .IsRequired(true);

            builder.Property(p => p.IsComplete)
                .IsRequired(true);

            builder.HasOne(p => p.ShoppingList)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.ShoppingListId)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName("FK_ShoppingListItem_ShoppingList_ShoppingListId");

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName("FK_ShoppingListItem_Product_ProductId");

            builder.HasOne(p => p.UnitSize)
                .WithMany()
                .HasForeignKey(p => p.UnitSizeId)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName("FK_ShoppingListItem_UnitSize_UnitSizeId");

        }
    }
}
