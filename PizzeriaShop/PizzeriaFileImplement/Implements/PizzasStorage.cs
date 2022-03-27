using PizzeriaFileImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class PizzasStorage : IPizzaStorage
    {
        private readonly FileDataListSingleton source;

        public PizzasStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<PizzaViewModel> GetFullList()
        {
            return source.Pizzas
                .Select(CreateModel)
                .ToList();
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Pizzas
                .Where(recPizza => recPizza.PizzaName.Contains(model.PizzaName))
                .Select(CreateModel)
                .ToList();
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            Pizza pizza = source.Pizzas.FirstOrDefault(recPizza => recPizza.PizzaName == model.PizzaName || recPizza.Id == model.Id);
            return pizza != null ? CreateModel(pizza) : null;
        }

        public void Insert(PizzaBindingModel model)
        {
            int maxId = source.Pizzas.Count > 0 ? source.Pizzas.Max(recPizza => recPizza.Id) : 0;
            var pizza = new Pizza
            {
                Id = maxId + 1,
                PizzaIngredients = new Dictionary<int, int>()
            };
            source.Pizzas.Add(CreateModel(model, pizza));
        }

        public void Update(PizzaBindingModel model)
        {
            Pizza pizza = source.Pizzas.FirstOrDefault(recPizza => recPizza.Id == model.Id);
            if (pizza == null)
            {
                throw new Exception("Пицца не найдена");
            }
            CreateModel(model, pizza);
        }

        public void Delete(PizzaBindingModel model)
        {
            Pizza pizza = source.Pizzas.FirstOrDefault(recPizza => recPizza.Id == model.Id);
            if (pizza != null)
            {
                source.Pizzas.Remove(pizza);
            }
            else
            {
                throw new Exception("Пицца не найдена");
            }
        }

        private Pizza CreateModel(PizzaBindingModel model, Pizza pizza)
        {
            pizza.PizzaName = model.PizzaName;
            pizza.Price = model.Price;

            foreach (var key in pizza.PizzaIngredients.Keys.ToList())
            {
                if (!model.PizzaIngredients.ContainsKey(key))
                {
                    pizza.PizzaIngredients.Remove(key);
                }
            }

            foreach (var ingredient in model.PizzaIngredients)
            {
                if (pizza.PizzaIngredients.ContainsKey(ingredient.Key))
                {
                    pizza.PizzaIngredients[ingredient.Key] = model.PizzaIngredients[ingredient.Key].Item2;
                }
                else
                {
                    pizza.PizzaIngredients.Add(ingredient.Key, model.PizzaIngredients[ingredient.Key].Item2);
                }
            }
            return pizza;
        }

        private PizzaViewModel CreateModel(Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = pizza.PizzaIngredients
                .ToDictionary(pizzaIngredient => pizzaIngredient.Key, pizzaIngredient =>
                (source.Ingredients.FirstOrDefault(Ingredient => Ingredient.Id == pizzaIngredient.Key)?.IngredientName, pizzaIngredient.Value))
            };
        }
    }
}
