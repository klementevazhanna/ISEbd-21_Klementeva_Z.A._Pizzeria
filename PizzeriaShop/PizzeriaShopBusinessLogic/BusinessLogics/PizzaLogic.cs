using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.BusinessLogics
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly IPizzaStorage _pizzaStorage;

        public PizzaLogic(IPizzaStorage pizzaStorage)
        {
            _pizzaStorage = pizzaStorage;
        }

        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            if (model == null)
            {
                return _pizzaStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PizzaViewModel> { _pizzaStorage.GetElement(model) };
            }
            return _pizzaStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PizzaBindingModel model)
        {
            var element = _pizzaStorage.GetElement(new PizzaBindingModel { PizzaName = model.PizzaName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть пицца с таким названием");
            }
            if (model.Id.HasValue)
            {
                _pizzaStorage.Update(model);
            }
            else
            {
                _pizzaStorage.Insert(model);
            }
        }

        public void Delete(PizzaBindingModel model)
        {
            var element = _pizzaStorage.GetElement(new PizzaBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _pizzaStorage.Delete(model);
        }
    }
}
