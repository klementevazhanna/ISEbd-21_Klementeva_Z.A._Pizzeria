using PizzeriaFileImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly FileDataListSingleton _source;

        public WareHouseStorage()
        {
            _source = FileDataListSingleton.GetInstance();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            return _source.WareHouses
                .Select(CreateModel)
                .ToList();
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return _source.WareHouses
                .Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
                .Select(CreateModel)
                .ToList();
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            WareHouse wareHouse = _source.WareHouses.FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName || rec.Id == model.Id);
            return wareHouse != null ? CreateModel(wareHouse) : null;
        }

        public void Insert(WareHouseBindingModel model)
        {
            int maxId = _source.WareHouses.Count > 0 ? _source.WareHouses.Max(rec => rec.Id) : 0;
            var wareHouse = new WareHouse
            {
                Id = maxId + 1,
                WareHouseIngredients = new Dictionary<int, int>()
            };

            _source.WareHouses.Add(CreateModel(model, wareHouse));
        }

        public void Update(WareHouseBindingModel model)
        {
            WareHouse wareHouse = _source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (wareHouse == null)
            {
                throw new Exception("Склад не найден");
            }

            CreateModel(model, wareHouse);
        }

        public void Delete(WareHouseBindingModel model)
        {
            WareHouse wareHouse = _source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (wareHouse != null)
            {
                _source.WareHouses.Remove(wareHouse);
            }
            else
            {
                throw new Exception("Склад не найден");
            }
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
                    wareHouse.WareHouseIngredients[ingredient.Key] = model.WareHouseIngredients[ingredient.Key].Item2;
                }
                else
                {
                    wareHouse.WareHouseIngredients.Add(ingredient.Key, model.WareHouseIngredients[ingredient.Key].Item2);
                }
            }
            return wareHouse;
        }

        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {

            var wareHouseIngredients = new Dictionary<int, (string, int)>();
            foreach (var sc in wareHouse.WareHouseIngredients)
            {
                string ingredientName = string.Empty;
                foreach (var ingredient in _source.Ingredients)
                {
                    if (sc.Key == ingredient.Id)
                    {
                        ingredientName = ingredient.IngredientName;
                        break;
                    }
                }
                wareHouseIngredients.Add(sc.Key, (ingredientName, sc.Value));
            }
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                ResponsiblePersonFIO = wareHouse.ResponsiblePersonFIO,
                DateCreate = wareHouse.DateCreate,
                WareHouseIngredients = wareHouseIngredients
            };
        }

        public bool WriteOffIngredients(Dictionary<int, (string, int)> pizzaIngredients, int pizzaCount)
        {
            foreach (var pizzaIngredient in pizzaIngredients)
            {
                int wareHouseIngredientCount = _source.WareHouses
                    .Where(wareHouse => wareHouse.WareHouseIngredients.ContainsKey(pizzaIngredient.Key))
                    .Sum(wareHouse => wareHouse.WareHouseIngredients[pizzaIngredient.Key]);

                if (wareHouseIngredientCount < (pizzaIngredient.Value.Item2 * pizzaCount))
                {
                    return false;
                }
            }

            foreach (var pizzaIngredient in pizzaIngredients)
            {
                int pizzaIngredientCount = pizzaIngredient.Value.Item2 * pizzaCount;

                var wareHousesWithPizzaIngredients = _source.WareHouses
                    .Where(wareHouse => wareHouse.WareHouseIngredients.ContainsKey(pizzaIngredient.Key));

                foreach (var wareHouse in wareHousesWithPizzaIngredients)
                {
                    if (wareHouse.WareHouseIngredients[pizzaIngredient.Key] <= pizzaIngredientCount)
                    {
                        pizzaIngredientCount -= wareHouse.WareHouseIngredients[pizzaIngredient.Key];
                        wareHouse.WareHouseIngredients.Remove(pizzaIngredient.Key);
                    }
                    else
                    {
                        wareHouse.WareHouseIngredients[pizzaIngredient.Key] -= pizzaIngredientCount;
                        pizzaIngredientCount = 0;
                    }

                    if (pizzaIngredientCount == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }
    }
}
