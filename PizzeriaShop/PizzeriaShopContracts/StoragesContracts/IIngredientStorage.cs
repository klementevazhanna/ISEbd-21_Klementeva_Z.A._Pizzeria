using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.StoragesContracts
{
    public interface IIngredientStorage
    {
        List<IngredientViewModel> GetFullList();

        List<IngredientViewModel> GetFilteredList(IngredientBindingModel model);

        IngredientViewModel GetElement(IngredientBindingModel model);

        void Insert(IngredientBindingModel model);

        void Update(IngredientBindingModel model);

        void Delete(IngredientBindingModel model);
    }
}
