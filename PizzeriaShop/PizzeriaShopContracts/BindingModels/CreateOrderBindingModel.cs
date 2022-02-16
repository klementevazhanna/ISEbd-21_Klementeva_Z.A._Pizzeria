namespace PizzeriaShopContracts.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int PizzaId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
