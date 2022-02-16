using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrder(CreateOrderBindingModel model);

        void TakeOrderInWork(ChangeStatusBindingModel model);

        void FinishOrder(ChangeStatusBindingModel model);

        void DeliveryOrder(ChangeStatusBindingModel model);
    }
}
