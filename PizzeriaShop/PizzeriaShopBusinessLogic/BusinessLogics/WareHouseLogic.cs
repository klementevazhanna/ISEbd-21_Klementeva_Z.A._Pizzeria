using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.BusinessLogics
{
    public class WareHouseLogic : IWareHouseLogic
    {
        private readonly IWareHouseStorage _wareHouseStorage;

        private readonly IIngredientStorage _ingredientStorage;

        public WareHouseLogic(IWareHouseStorage wareHouseStorage, IIngredientStorage inredientsStorage)
        {
            _wareHouseStorage = wareHouseStorage;
            _ingredientStorage = inredientsStorage;
        }

        public List<WareHouseViewModel> Read(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return _wareHouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WareHouseViewModel> { _wareHouseStorage.GetElement(model) };
            }
            return _wareHouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                WareHouseName = model.WareHouseName
            });
            if (element != null && element.WareHouseName == model.WareHouseName)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _wareHouseStorage.Update(model);
            }
            else
            {
                _wareHouseStorage.Insert(model);
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _wareHouseStorage.Delete(model);
        }

        public void ReplenishByComponent(WareHouseReplenishmentBindingModel model)
        {
            var wareHouse = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.WareHouseId
            });
            if (wareHouse == null)
            {
                throw new Exception("Не найден склад");
            }
            var ingredient = _ingredientStorage.GetElement(new IngredientBindingModel
            {
                Id = model.IngredientId
            });
            if (ingredient == null)
            {
                throw new Exception("Не найден компонент");
            }
            if (wareHouse.WareHouseIngredients.ContainsKey(model.IngredientId))
            {
                wareHouse.WareHouseIngredients[model.IngredientId] =
                (ingredient.IngredientName, wareHouse.WareHouseIngredients[model.IngredientId].Item2 + model.Count);
            }
            else
            {
                wareHouse.WareHouseIngredients.Add(ingredient.Id, (ingredient.IngredientName, model.Count));
            }
            _wareHouseStorage.Update(new WareHouseBindingModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsiblePersonFIO = wareHouse.ResponsiblePersonFIO,
                DateCreate = wareHouse.DateCreate,
                WareHouseIngredients = wareHouse.WareHouseIngredients
            });
        }
    }
}
