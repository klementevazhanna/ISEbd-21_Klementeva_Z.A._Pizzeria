namespace PizzeriaShopContracts.BindingModels
{
    public class WareHouseReplenishmentBindingModel
    {
        public int WareHouseId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
