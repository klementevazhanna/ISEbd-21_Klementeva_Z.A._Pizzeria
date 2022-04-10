﻿using PizzeriaFileImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly FileDataListSingleton _source;

        public OrderStorage()
        {
            _source = FileDataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetFullList()
        {
            return _source.Orders
                .Select(CreateModel)
                .ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return _source.Orders
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                (model.SearchStatus.HasValue && model.SearchStatus.Value == rec.Status) ||
                (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && model.Status == rec.Status))
                .Select(CreateModel)
                .ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            Order order = _source.Orders.FirstOrDefault(recOder => recOder.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            int maxId = _source.Orders.Count > 0 ? _source.Orders.Max(recOder => recOder.Id) : 0;
            var order = new Order { Id = maxId + 1 };
            _source.Orders.Add(CreateModel(model, order));
        }

        public void Update(OrderBindingModel model)
        {
            Order order = _source.Orders.FirstOrDefault(recOder => recOder.Id == model.Id);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            CreateModel(model, order);
        }

        public void Delete(OrderBindingModel model)
        {
            Order order = _source.Orders.FirstOrDefault(recOrder => recOrder.Id == model.Id);
            if (order != null)
            {
                _source.Orders.Remove(order);
            }
            else
            {
                throw new Exception("Заказ не найден");
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ClientId = (int)model.ClientId;
            order.PizzaId = model.PizzaId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.ImplementerId = model.ImplementerId;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                PizzaName = _source.Pizzas.FirstOrDefault(pizza => pizza.Id == order.PizzaId)?.PizzaName,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                ClientId = order.ClientId,
                ClientFIO = _source.Clients.FirstOrDefault(rec => rec.Id == order.ClientId)?.ClientFIO,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = _source.Implementers.FirstOrDefault(x => x.Id == order.ImplementerId)?.ImplementerFIO
            };
        }
    }
}
