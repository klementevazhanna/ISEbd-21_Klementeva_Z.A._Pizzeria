using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoWareHouses
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportWareHouseIngredientViewModel> WareHouseIngredients { get; set; }
    }
}
