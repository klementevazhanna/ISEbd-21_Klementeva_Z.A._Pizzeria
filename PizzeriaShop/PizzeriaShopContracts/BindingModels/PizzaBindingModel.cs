using System.Collections.Generic;

namespace PizzeriaShopContracts.BindingModels
{
    public class PizzaBindingModel
    {
        public int? Id { get; set; }

        public string PizzaName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> PizzaIngredients { get; set; }
    }
}
