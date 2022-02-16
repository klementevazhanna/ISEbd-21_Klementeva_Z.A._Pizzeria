using PizzaShopListImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopListImplement.Implements
{
    public class PizzasStorage : IPizzaStorage
    {
        private readonly DataListSingleton source;

        public PizzasStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<PizzaViewModel> GetFullList()
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var pizza in source.Pizzas)
            {
                result.Add(CreateModel(pizza));
            }
            return result;
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.PizzaName.Contains(model.PizzaName))
                {
                    result.Add(CreateModel(pizza));
                }
            }
            return result;
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id || pizza.PizzaName ==
                model.PizzaName)
                {
                    return CreateModel(pizza);
                }
            }
            return null;
        }

        public void Insert(PizzaBindingModel model)
        {
            Pizza tempPizza = new Pizza
            {
                Id = 1,
                PizzaIngredients = new Dictionary<int, int>()
            };
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id >= tempPizza.Id)
                {
                    tempPizza.Id = pizza.Id + 1;
                }
            }
            source.Pizzas.Add(CreateModel(model, tempPizza));
        }

        public void Update(PizzaBindingModel model)
        {
            Pizza tempPizza = null;
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id)
                {
                    tempPizza = pizza;
                }
            }
            if (tempPizza == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempPizza);
        }

        public void Delete(PizzaBindingModel model)
        {
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
            foreach (var pizzas in model.PizzaIngredients)
            {
                if (pizza.PizzaIngredients.ContainsKey(pizzas.Key))
                {
                    pizza.PizzaIngredients[pizzas.Key] =
                    model.PizzaIngredients[pizzas.Key].Item2;
                }
                else
                {
                    pizza.PizzaIngredients.Add(pizzas.Key,
                    model.PizzaIngredients[pizzas.Key].Item2);
                }
            }
            return pizza;
        }

        private PizzaViewModel CreateModel(Pizza pizza)
        {

            Dictionary<int, (string, int)> PizzaIngredients = new
            Dictionary<int, (string, int)>();
            foreach (var ic in pizza.PizzaIngredients)
            {
                string pizzaName = string.Empty;
                foreach (var pizzas in source.Pizzas)
                {
                    if (ic.Key == pizzas.Id)
                    {
                        pizzaName = pizzas.PizzaName;
                        break;
                    }
                }
                PizzaIngredients.Add(ic.Key, (pizzaName, ic.Value));
            }
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = PizzaIngredients
            };
        }
    }
}
