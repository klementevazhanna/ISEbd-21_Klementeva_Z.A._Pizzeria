using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);

        void CreateOrUpdate(IngredientBindingModel model);

        void Delete(IngredientBindingModel model);
    }
}
