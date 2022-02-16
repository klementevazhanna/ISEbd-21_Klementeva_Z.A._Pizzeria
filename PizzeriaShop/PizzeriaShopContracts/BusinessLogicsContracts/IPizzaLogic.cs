using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IPizzaLogic
    {
        List<PizzaViewModel> Read(PizzaBindingModel model);

        void CreateOrUpdate(PizzaBindingModel model);

        void Delete(PizzaBindingModel model);
    }
}
