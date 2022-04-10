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
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";

        private readonly string WareHouseFileName = "WareHouse.xml";

        public List<Ingredient> Ingredients { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<Order> Orders { get; set; }

        public List<WareHouse> WareHouses { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Pizzas = LoadPizzas();
            Orders = LoadOrders();
            WareHouses = LoadWareHouses();
            Clients = LoadClients();
            Implementers = LoadImplementers();
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
            SaveWareHouses();
            SaveClients();
            SaveImplementers();
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

                    foreach (var ingredient in pizza.Element("PizzaIngredients").Elements("PizzaIngredient").ToList())
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
                        ClientId = Convert.ToInt32(order.Element("ClientId").Value),
                        PizzaId = Convert.ToInt32(order.Element("PizzaId").Value),
                        ImplementerId = Convert.ToInt32(order.Element("ImplementerId").Value),
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

        private List<WareHouse> LoadWareHouses()
        {
            var list = new List<WareHouse>();

            if (File.Exists(WareHouseFileName))
            {
                XDocument xDocument = XDocument.Load(WareHouseFileName);
                var xElements = xDocument.Root.Elements("WareHouse").ToList();

                foreach (var elem in xElements)
                {
                    var wareHouseIngredients = new Dictionary<int, int>();
                    foreach (var ingredient in elem.Element("WareHouseIngredients").Elements("WareHouseIngredient").ToList())
                    {
                        wareHouseIngredients.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }

                    list.Add(new WareHouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WareHouseName = elem.Element("WareHouseName").Value,
                        ResponsiblePersonFIO = elem.Element("ResponsiblePersonFIO").Value,
                        WareHouseIngredients = wareHouseIngredients
                    });

                    if (elem.Element("DateCreate").Value != "")
                    {
                        list.Last().DateCreate = DateTime.ParseExact(elem.Element("DateCreate").Value, "d.M.yyyy H:m:s", null);
                    }
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var client in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(client.Attribute("Id").Value),
                        ClientFIO = client.Element("ClientFIO").Value,
                        Email = client.Element("Email").Value,
                        Password = client.Element("Password").Value,
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
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
                        new XElement("ClientId", order.ClientId),
                        new XElement("PizzaId", order.PizzaId),
                        new XElement("ImplementerId", order.ImplementerId),
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

        private void SaveWareHouses()
        {
            if (WareHouses != null)
            {
                var xElement = new XElement("WareHouses");

                foreach (var wareHouse in WareHouses)
                {
                    var ingredients = new XElement("WareHouseIngredients");

                    foreach (var ingredient in wareHouse.WareHouseIngredients)
                    {
                        ingredients.Add(new XElement(
                            "WareHouseIngredient",
                            new XElement("Key", ingredient.Key),
                            new XElement("Value", ingredient.Value)
                        ));
                    }

                    xElement.Add(new XElement(
                        "WareHouse",
                        new XAttribute("Id", wareHouse.Id),
                        new XElement("WareHouseName", wareHouse.WareHouseName),
                        new XElement("ResponsiblePersonFIO", wareHouse.ResponsiblePersonFIO),
                        new XElement("DateCreate", wareHouse.DateCreate.ToString()),
                        ingredients
                    ));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WareHouseFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");

                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }
}
