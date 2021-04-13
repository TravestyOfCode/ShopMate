namespace ShopMate.Data
{
    public class ShoppingListItem : EntityBase
    {
        public string ShoppingListId { get; set; }

        public ShoppingList ShoppingList { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public string UnitSizeId { get; set; }

        public UnitSize UnitSize { get; set; }

        public bool IsComplete { get; set; }
    }
}
