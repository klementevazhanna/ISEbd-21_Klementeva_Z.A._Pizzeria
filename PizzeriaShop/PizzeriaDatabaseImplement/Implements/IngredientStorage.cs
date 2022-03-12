using PizzeriaDatabaseImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        public List<IngredientViewModel> GetFullList()
        {
            using (var context = new PizzeriaShopDatabase())
            {
                return context.Ingredients
                .Select(CreateModel)
                .ToList();
            }
        }

        public List<IngredientViewModel> GetFilteredList(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                return context.Ingredients
                    .Where(rec => rec.IngredientName.Contains(model.IngredientName))
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public IngredientViewModel GetElement(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                Ingredient ingredient = context.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName || rec.Id == model.Id);
                return ingredient != null ? CreateModel(ingredient) : null;
            }
        }

        public void Insert(IngredientBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                context.Ingredients.Add(CreateModel(model, new Ingredient()));
                context.SaveChanges();
            }
        }

        public void Update(IngredientBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                Ingredient ingredient = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                if (ingredient == null)
                {
                    throw new Exception("Элемент не найден");
                }

                CreateModel(model, ingredient);
                context.SaveChanges();
            }
        }

        public void Delete(IngredientBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                Ingredient ingredient = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                if (ingredient != null)
                {
                    context.Ingredients.Remove(ingredient);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private static Ingredient CreateModel(IngredientBindingModel model, Ingredient ingredient)
        {
            ingredient.IngredientName = model.IngredientName;
            return ingredient;
        }

        private static IngredientViewModel CreateModel(Ingredient ingredient)
        {
            return new IngredientViewModel
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName
            };
        }
    }
}
