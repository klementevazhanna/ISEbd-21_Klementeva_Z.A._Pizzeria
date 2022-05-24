using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.Enums;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzeriaShopBusinessLogic.BusinessLogics
{
    public class WorkModeling : IWorkProcess
    {
        private IOrderLogic _orderLogic;

        private readonly Random _rnd;

        public WorkModeling()
        {
            _rnd = new Random(1000);
        }

        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
            var implementers = implementerLogic.Read(null);

            ConcurrentBag<OrderViewModel> orders = new(_orderLogic.Read(new OrderBindingModel { SearchStatus = OrderStatus.Принят }));

            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, orders));
            }
        }

        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));

            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * _rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }

            // ищем заказы, которым нужны ингредиенты (может быть они появились)
            var neededIngredientsOrders = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Требуются_материалы
            }));

            foreach (var order in neededIngredientsOrders)
            {
                _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });

                var currentOrder = _orderLogic.Read(new OrderBindingModel { Id = order.Id })[0];
                if (currentOrder.Status == OrderStatus.Требуются_материалы)
                {
                    continue;
                }

                // делаем работу
                Thread.Sleep(implementer.WorkingTime * _rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }

            await Task.Run(() =>
            {
                while (!orders.IsEmpty)
                {
                    if (orders.TryTake(out OrderViewModel order))
                    {
                        // пытаемся назначить заказ на исполнителя
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });

                        var currentOrder = _orderLogic.Read(new OrderBindingModel{ Id = order.Id })[0];
                        if (currentOrder.Status == OrderStatus.Требуются_материалы)
                        {
                            continue;
                        }

                        // делаем работу
                        Thread.Sleep(implementer.WorkingTime * _rnd.Next(1, 5) * order.Count);
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                        // отдыхаем
                        Thread.Sleep(implementer.PauseTime);
                    }
                }
            });
        }
    }
}
