using System;
using System.Collections.Generic;

namespace ShopMate.Data
{
    public class ShoppingList : EntityBase
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Title { get; set; }

        public DateTime TripDate { get; set; }

        public string Store { get; set; }

        public List<ShoppingListItem> Items { get; set; }
    }
}