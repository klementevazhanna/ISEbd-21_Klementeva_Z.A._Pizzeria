using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<PizzaViewModel> Pizzas { get; set; }
    }
}
