namespace ShopMate.Data
{
    public class ShoppingListItem : EntityBase
    {
        public int ShoppingListId { get; set; }

        public ShoppingList ShoppingList { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public int UnitSizeId { get; set; }

        public UnitSize UnitSize { get; set; }

        public bool IsComplete { get; set; }
    }
}
