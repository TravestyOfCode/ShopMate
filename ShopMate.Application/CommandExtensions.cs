namespace ShopMate.Application
{
    public static class CommandExtensions
    {
        public static Data.UnitSize ToUnitSize(this UnitSizes.Create.Command command)
        {
            if(command == null)
            {
                return null;
            }
            return new Data.UnitSize()
            {
                Name = command.Name 
            };
        }

        public static Data.Product ToProduct(this Products.Create.Command command)
        {
            if(command == null)
            {
                return null;
            }
            return new Data.Product()
            {
                Name = command.Name,
                DefaultUnitSizeId = command.DefaultUnitSizeId
            };
        }
    }
}
