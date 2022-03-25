﻿using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new PizzeriaShopDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Pizza)
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PizzaId = rec.PizzaId,
                        PizzaName = rec.Pizza.PizzaName,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status,
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement,
                    })
                    .ToList();
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Pizza)
                    .Where(rec=>rec.PizzaId == model.PizzaId ||
                        (model.DateFrom.GetHashCode() != 0 && model.DateTo.GetHashCode() != 0 && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PizzaId = rec.PizzaId,
                        PizzaName = rec.Pizza.PizzaName,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status,
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement,
                    })
                    .ToList();
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                Order order = context.Orders.Include(rec => rec.Pizza).FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    PizzaId = order.PizzaId,
                    PizzaName = order.Pizza.PizzaName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                var order = new Order
                {
                    PizzaId = model.PizzaId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Элемент не найден");
                }
                order.PizzaId = model.PizzaId;
                order.Count = model.Count;
                order.Sum = model.Sum;
                order.Status = model.Status;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;

                CreateModel(model, order);
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new PizzeriaShopDatabase())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaShopDatabase())
            {
                Pizza pizza = context.Pizzas.FirstOrDefault(rec => rec.Id == model.PizzaId);
                if (pizza != null)
                {
                    if (pizza.Orders == null)
                    {
                        pizza.Orders = new List<Order>();
                    }

                    pizza.Orders.Add(order);
                    context.Pizzas.Update(pizza);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}
