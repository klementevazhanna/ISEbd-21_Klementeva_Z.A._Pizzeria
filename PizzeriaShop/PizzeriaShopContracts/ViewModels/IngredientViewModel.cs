using PizzeriaShopContracts.Attributes;
using System.ComponentModel;

namespace PizzeriaShopContracts.ViewModels
{
    public class IngredientViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Ингредиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
