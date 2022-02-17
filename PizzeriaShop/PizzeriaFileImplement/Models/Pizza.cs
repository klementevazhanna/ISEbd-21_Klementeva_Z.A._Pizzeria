using System.Collections.Generic;

namespace PizzeriaFileImplement.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        public string PizzaName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, int> PizzaIngredients { get; set; }
    }
}
