using PizzaShopListImplement.Models;
using System.Collections.Generic;

namespace PizzaShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<WareHouse> Warehouses { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> Messages { get; set; }

        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
            Clients = new List<Client>();
            Warehouses = new List<WareHouse>();
            Implementers = new List<Implementer>();
            Messages = new List<MessageInfo>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
