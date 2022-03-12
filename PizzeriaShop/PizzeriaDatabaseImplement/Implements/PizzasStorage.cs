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
    public class PizzasStorage : IPizzaStorage
    {
        public List<PizzaViewModel> GetFullList()
        {
            using (var context = new PizzeriaShopDatabase())
            {
                return context.Pizzas
                    .Include(rec => rec.PizzaIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .ToList()
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                return context.Pizzas
                    .Include(rec => rec.PizzaIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .Where(rec => rec.PizzaName.Contains(model.PizzaName))
                    .ToList()
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                Pizza pizza = context.Pizzas
                    .Include(rec => rec.PizzaIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .FirstOrDefault(rec => rec.PizzaName == model.PizzaName || rec.Id == model.Id);

                return pizza != null ? CreateModel(pizza) : null;
            }
        }

        public void Insert(PizzaBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var pizza = new Pizza
                        {
                            PizzaName = model.PizzaName,
                            Price = model.Price
                        };
                        context.Pizzas.Add(pizza);
                        context.SaveChanges();

                        CreateModel(model, pizza, context);
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

        public void Update(PizzaBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Pizza pizza = context.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                        if (pizza == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        pizza.PizzaName = model.PizzaName;
                        pizza.Price = model.Price;

                        CreateModel(model, pizza, context);
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

        public void Delete(PizzaBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                Pizza pizza = context.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                if (pizza != null)
                {
                    context.Pizzas.Remove(pizza);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private static Pizza CreateModel(PizzaBindingModel model, Pizza pizza, PizzeriaShopDatabase context)
        {
            if (model.Id.HasValue)
            {
                var pizzaIngredients = context.PizzaIngredients.Where(rec => rec.PizzaId == model.Id.Value).ToList();
                context.PizzaIngredients.RemoveRange(pizzaIngredients.Where(rec => !model.PizzaIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                foreach (var updateIngredient in pizzaIngredients)
                {
                    updateIngredient.Count = model.PizzaIngredients[updateIngredient.IngredientId].Item2;
                    model.PizzaIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }

            foreach (var pc in model.PizzaIngredients)
            {
                context.PizzaIngredients.Add(new PizzaIngredient
                {
                    PizzaId = pizza.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });

                context.SaveChanges();
            }

            return pizza;
        }

        private static PizzaViewModel CreateModel(Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = pizza.PizzaIngredients
                .ToDictionary(recPC => recPC.IngredientId, recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
            };
        }
    }
}
