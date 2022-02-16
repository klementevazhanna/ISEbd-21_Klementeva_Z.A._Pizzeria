using System.ComponentModel;

namespace PizzeriaShopContracts.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
