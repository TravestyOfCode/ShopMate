namespace ShopMate.Data
{
    public class Product : EntityBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DefaultUnitSizeId { get; set; }

        public UnitSize DefaultUnitSize { get; set; }
    }
}
