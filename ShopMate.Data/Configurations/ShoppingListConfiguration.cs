using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public class ShoppingListConfiguration : EntityBaseConfiguration<ShoppingList>
    {
        public override void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(ShoppingList));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired(true);

            builder.Property(p => p.Title)
                .HasMaxLength(64)
                .IsRequired(true);

            builder.Property(p => p.TripDate)
                .IsRequired(true)
                .HasColumnType("DATE");

            builder.Property(p => p.Store)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName("FK_ShoppingList_AspNetUser_UserId");                
        }
    }
}
