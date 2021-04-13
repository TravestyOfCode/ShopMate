namespace ShopMate.Data
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DefaultUnitSizeId { get; set; }

        public UnitSize DefaultUnitSize { get; set; }
    }
}
