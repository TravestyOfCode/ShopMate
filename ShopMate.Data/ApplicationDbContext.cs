using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUser _user;

        public DbSet<ShoppingList> ShoppingLists { get; set; }

        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<UnitSize> UnitSizes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser user) : base(options)
        {
            _user = user;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            AddGeneratedValues();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddGeneratedValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddGeneratedValues()
        {
            var entities = ChangeTracker.Entries().Where(p => p.Entity is EntityBase && (p.State == EntityState.Added || p.State == EntityState.Modified));

            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            if (_user.UserId == null)
            {
                throw new NullReferenceException("UserId is null.");
            }

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedById = _user.UserId;
                    ((EntityBase)entity.Entity).CreatedAt = DateTimeOffset.Now;
                }

                ((EntityBase)entity.Entity).LastModifiedById = _user.UserId;
                ((EntityBase)entity.Entity).LastModifiedAt = DateTimeOffset.Now;

                var curValues = entity.CurrentValues;
            }
        }
    }
}
