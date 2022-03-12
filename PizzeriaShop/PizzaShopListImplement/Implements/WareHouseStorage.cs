using PizzaShopListImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopListImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly DataListSingleton source;

        public WareHouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.Warehouses)
            {
                result.Add(CreateModel(wareHouse));
            }
            return result;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.WareHouseName.Contains(model.WareHouseName))
                {
                    result.Add(CreateModel(wareHouse));
                }
            }
            return result;
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id == model.Id || wareHouse.WareHouseName ==
                model.WareHouseName)
                {
                    return CreateModel(wareHouse);
                }
            }
            return null;
        }

        public void Insert(WareHouseBindingModel model)
        {
            var tempWareHouse = new WareHouse
            {
                Id = 1,
                WareHouseIngredients = new Dictionary<int, int>(),
                DateCreate = model.DateCreate
            };
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = wareHouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWareHouse));
        }

        public void Update(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = null;
            foreach (var wareHouse in source.Warehouses)
            {
                if (wareHouse.Id == model.Id)
                {
                    tempWareHouse = wareHouse;
                }
            }
            if (tempWareHouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWareHouse);
        }

        public void Delete(WareHouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.ResponsiblePersonFIO = model.ResponsiblePersonFIO;
            wareHouse.DateCreate = model.DateCreate;
            foreach (var key in wareHouse.WareHouseIngredients.Keys.ToList())
            {
                if (!model.WareHouseIngredients.ContainsKey(key))
                {
                    wareHouse.WareHouseIngredients.Remove(key);
                }
            }
            foreach (var ingredient in model.WareHouseIngredients)
            {
                if (wareHouse.WareHouseIngredients.ContainsKey(ingredient.Key))
                {
                    wareHouse.WareHouseIngredients[ingredient.Key] =
                    model.WareHouseIngredients[ingredient.Key].Item2;
                }
                else
                {
                    wareHouse.WareHouseIngredients.Add(ingredient.Key,
                    model.WareHouseIngredients[ingredient.Key].Item2);
                }
            }
            return wareHouse;
        }

        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            var wareHouseComponents = new Dictionary<int, (string, int)>();

            foreach (var whi in wareHouse.WareHouseIngredients)
            {
                string ingridientName = string.Empty;
                foreach (var ingredient in source.Ingredients)
                {
                    if (whi.Key == ingredient.Id)
                    {
                        ingridientName = ingredient.IngredientName;
                        break;
                    }
                }
                wareHouseComponents.Add(whi.Key, (ingridientName, whi.Value));
            }
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsiblePersonFIO = wareHouse.ResponsiblePersonFIO,
                DateCreate = wareHouse.DateCreate,
                WareHouseIngredients = wareHouseComponents
            };
        }

        public bool WriteOffComponents(Dictionary<int, (string, int)> pizzaIngredients, int pizzaCount)
        {
            throw new NotImplementedException();
        }
    }
}
