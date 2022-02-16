using PizzeriaFileImplement.Models;
using PizzeriaShopContracts.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PizzeriaFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string IngredientFileName = "Ingredient.xml";
        private readonly string PizzaFileName = "Pizza.xml";
        private readonly string OrderFileName = "Order.xml";

        public List<Ingredient> Ingredients { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<Order> Orders { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Pizzas = LoadPizzas();
            Orders = LoadOrders();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }

            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveIngredients();
            SavePizzas();
            SaveOrders();
        }

        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();

            if (File.Exists(IngredientFileName))
            {
                XDocument xDocument = XDocument.Load(IngredientFileName);

                var xElements = xDocument.Root.Elements("Ingredient").ToList();

                foreach (var ingredient in xElements)
                {
                    list.Add(new Ingredient
                    {
                        Id = Convert.ToInt32(ingredient.Attribute("Id").Value),
                        IngredientName = ingredient.Element("IngredientName").Value
                    });
                }
            }
            return list;
        }

        private List<Pizza> LoadPizzas()
        {
            var list = new List<Pizza>();

            if (File.Exists(PizzaFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaFileName);

                var xElements = xDocument.Root.Elements("Pizza").ToList();

                foreach (var pizza in xElements)
                {
                    var pizzaIngredients = new Dictionary<int, int>();

                    foreach (var ingredient in pizza.Element("PizzaIngredients").Elements("PizzaIngredients").ToList())
                    {
                        pizzaIngredients.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }

                    list.Add(new Pizza
                    {
                        Id = Convert.ToInt32(pizza.Attribute("Id").Value),
                        PizzaName = pizza.Element("PizzaName").Value,
                        Price = Convert.ToDecimal(pizza.Element("Price").Value),
                        PizzaIngredients = pizzaIngredients
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();

            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);

                var xElements = xDocument.Root.Elements("Order").ToList();

                foreach (var order in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(order.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(order.Element("PizzaId").Value),
                        Count = Convert.ToInt32(order.Element("Count").Value),
                        Sum = Convert.ToDecimal(order.Element("Sum").Value),
                        Status = (OrderStatus)Convert.ToInt32(order.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(order.Element("DateCreate").Value),
                        DateImplement = !string.IsNullOrEmpty(order.Element("DateImplement").Value) ? Convert.ToDateTime(order.Element("DateImplement").Value) : (DateTime?)null
                    });
                }
            }
            return list;
        }

        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");

                foreach (var ingredient in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                        new XAttribute("Id", ingredient.Id),
                        new XElement("IngredientName", ingredient.IngredientName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(IngredientFileName);
            }
        }

        private void SavePizzas()
        {
            if (Pizzas != null)
            {
                var xElement = new XElement("Pizzas");

                foreach (var pizza in Pizzas)
                {
                    var ingredientsElement = new XElement("PizzaIngredients");

                    foreach (var ingredient in pizza.PizzaIngredients)
                    {
                        ingredientsElement.Add(new XElement("PizzaIngredient",
                            new XElement("Key", ingredient.Key),
                            new XElement("Value", ingredient.Value)));
                    }

                    xElement.Add(new XElement("Pizza",
                        new XAttribute("Id", pizza.Id),
                        new XElement("PizzaName", pizza.PizzaName),
                        new XElement("Price", pizza.Price),
                        ingredientsElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PizzaFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");

                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("PizzaId", order.PizzaId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", (int)order.Status),
                        new XElement("DateCreate", order.DateCreate.ToString()),
                        new XElement("DateImplement", order.DateImplement.ToString())));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
    }
}
