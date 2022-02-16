using System;
using System.Collections.Generic;
using PizzaShopListImplement.Models;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizzas { get; set; }
        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
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

