using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;

namespace PizzeriaDatabaseImplement
{
    public class PizzeriaShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=pizzeriashop;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Ingredient> Ingredients { set; get; }

        public virtual DbSet<Pizza> Pizzas { set; get; }

        public virtual DbSet<PizzaIngredient> PizzaIngredients { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<WareHouse> WareHouses { set; get; }

        public virtual DbSet<WareHouseIngredient> WareHouseIngredients { set; get; }

        public virtual DbSet<Client> Clients { set; get; }
    }
}
