using System;

namespace ShopMate.Data
{
    public abstract class EntityBase
    {
        public string CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string LastModifiedById { get; set; }

        public ApplicationUser LastModifiedBy { get; set; }

        public DateTimeOffset LastModifiedAt { get; set; }
    }
}
