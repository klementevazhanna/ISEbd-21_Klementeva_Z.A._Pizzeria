using System;
using System.Collections.Generic;

namespace PizzeriaShopContracts.ViewModels
{
    public class ReportWareHouseIngredientViewModel
    {
        public string WareHouseName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Ingredients { get; set; }
    }
}
