﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShopListImplement.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> PizzaIngredients { get; set; }
    }
}
