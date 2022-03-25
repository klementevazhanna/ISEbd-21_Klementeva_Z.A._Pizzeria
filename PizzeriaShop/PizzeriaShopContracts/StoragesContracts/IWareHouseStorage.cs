using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.StoragesContracts
{
    public interface IWareHouseStorage
    {
        List<WareHouseViewModel> GetFullList();

        List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model);

        WareHouseViewModel GetElement(WareHouseBindingModel model);

        void Insert(WareHouseBindingModel model);

        void Update(WareHouseBindingModel model);

        void Delete(WareHouseBindingModel model);

        bool WriteOffIngredients(Dictionary<int, (string, int)> pizzaIngredients, int pizzaCount);
    }
}
