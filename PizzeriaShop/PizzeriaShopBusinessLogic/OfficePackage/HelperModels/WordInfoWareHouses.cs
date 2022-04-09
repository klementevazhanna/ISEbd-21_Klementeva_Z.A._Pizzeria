using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoWareHouses
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<WareHouseViewModel> WareHouses { get; set; }
    }
}
