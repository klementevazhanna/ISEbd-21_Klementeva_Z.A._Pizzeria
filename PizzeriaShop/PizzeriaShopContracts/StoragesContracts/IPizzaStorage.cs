using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.StoragesContracts
{
    public interface IPizzaStorage
    {
        List<PizzaViewModel> GetFullList();

        List<PizzaViewModel> GetFilteredList(PizzaBindingModel model);

        PizzaViewModel GetElement(PizzaBindingModel model);

        void Insert(PizzaBindingModel model);

        void Update(PizzaBindingModel model);

        void Delete(PizzaBindingModel model);
    }
}
