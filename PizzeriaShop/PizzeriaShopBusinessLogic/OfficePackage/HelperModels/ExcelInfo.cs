using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportPizzaIngredientViewModel> PizzaIngredients { get; set; }
    }
}
