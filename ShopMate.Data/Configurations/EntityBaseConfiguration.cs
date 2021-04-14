using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopMate.Data.Configurations
{
    public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedAt)
                .IsRequired(true);

            builder.Property(p => p.CreatedById)
                .IsRequired(true);

            builder.Property(p => p.LastModifiedAt)
                .IsRequired(true);

            builder.Property(p => p.LastModifiedById)
                .IsRequired(true);

            builder.HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName($"FK_{typeof(T).Name}_AspNetUser_CreatedById")
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(p => p.LastModifiedBy)
                .WithMany()
                .HasForeignKey(p => p.LastModifiedById)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName($"FK_{typeof(T).Name}_AspNetUser_LastModifiedById")
                .OnDelete(DeleteBehavior.ClientNoAction);

        }
    }
}
