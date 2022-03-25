using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        public List<WareHouseViewModel> GetFullList()
        {
            using (var context = new PizzeriaShopDatabase())
            {
                return context.WareHouses
                    .Include(rec => rec.WareHouseIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .ToList()
                    .Select(rec => new WareHouseViewModel
                    {
                        Id = rec.Id,
                        WareHouseName = rec.WareHouseName,
                        ResponsiblePersonFIO = rec.ResponsiblePersonFIO,
                        DateCreate = rec.DateCreate,
                        WareHouseIngredients = rec.WareHouseIngredients
                    .ToDictionary(recSC => recSC.IngredientId, recSC => (recSC.Ingredient?.IngredientName, recSC.Count))
                    })
                    .ToList();
            }
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                return context.WareHouses
                    .Include(rec => rec.WareHouseIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
                    .ToList()
                    .Select(rec => new WareHouseViewModel
                    {
                        Id = rec.Id,
                        WareHouseName = rec.WareHouseName,
                        ResponsiblePersonFIO = rec.ResponsiblePersonFIO,
                        DateCreate = rec.DateCreate,
                        WareHouseIngredients = rec.WareHouseIngredients
                    .ToDictionary(recWHI => recWHI.IngredientId, recWHI => (recWHI.Ingredient?.IngredientName, recWHI.Count))
                    })
                    .ToList();
            }
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                var wareHouse = context.WareHouses
                    .Include(rec => rec.WareHouseIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName || rec.Id == model.Id);
                return wareHouse != null ?
                new WareHouseViewModel
                {
                    Id = wareHouse.Id,
                    WareHouseName = wareHouse.WareHouseName,
                    ResponsiblePersonFIO = wareHouse.ResponsiblePersonFIO,
                    DateCreate = wareHouse.DateCreate,
                    WareHouseIngredients = wareHouse.WareHouseIngredients
                    .ToDictionary(rec => rec.IngredientId, rec => (rec.Ingredient?.IngredientName, rec.Count))
                } :
                null;
            }
        }

        public void Insert(WareHouseBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var wareHouse = new WareHouse
                        {
                            WareHouseName = model.WareHouseName,
                            ResponsiblePersonFIO = model.ResponsiblePersonFIO,
                            DateCreate = model.DateCreate
                        };
                        context.WareHouses.Add(wareHouse);
                        context.SaveChanges();

                        CreateModel(model, wareHouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(WareHouseBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        WareHouse wareHouse = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
                        if (wareHouse == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        wareHouse.WareHouseName = model.WareHouseName;
                        wareHouse.ResponsiblePersonFIO = model.ResponsiblePersonFIO;

                        CreateModel(model, wareHouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                WareHouse wareHouse = context.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (wareHouse != null)
                {
                    context.WareHouses.Remove(wareHouse);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse, PizzeriaShopDatabase context)
        {
            if (model.Id.HasValue)
            {
                var wareHouseIngredients = context.WareHouseIngredients.Where(rec => rec.WareHouseId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.WareHouseIngredients.RemoveRange(wareHouseIngredients.Where(rec => !model.WareHouseIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in wareHouseIngredients)
                {
                    updateIngredient.Count = model.WareHouseIngredients[updateIngredient.IngredientId].Item2;
                    model.WareHouseIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var whi in model.WareHouseIngredients)
            {
                context.WareHouseIngredients.Add(new WareHouseIngredient
                {
                    WareHouseId = wareHouse.Id,
                    IngredientId = whi.Key,
                    Count = whi.Value.Item2
                });
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception("Возникла ошибка при сохранении");
                }
            }
            return wareHouse;
        }

        public bool WriteOffIngredients(Dictionary<int, (string, int)> pizzaIngredients, int pizzaCount)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var pi in pizzaIngredients)
                        {
                            int pizzaIngredientCount = pi.Value.Item2 * pizzaCount;
                            var wareHouseIngredients = context.WareHouseIngredients.Where(wareHouseIngredient => wareHouseIngredient.IngredientId == pi.Key).ToList();

                            if (wareHouseIngredients.Sum(wareHouseIngredient => wareHouseIngredient.Count) < pizzaIngredientCount)
                            {
                                throw new Exception("Недостаточно ингредиентов");
                            }

                            foreach (var whi in wareHouseIngredients)
                            {
                                if (whi.Count <= pizzaIngredientCount)
                                {
                                    pizzaIngredientCount -= whi.Count;
                                    context.WareHouseIngredients.Remove(whi);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    whi.Count -= pizzaIngredientCount;
                                    context.SaveChanges();
                                    pizzaIngredientCount = 0;
                                }

                                if (pizzaIngredientCount == 0)
                                {
                                    break;
                                }
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
