using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportPizzaIngredientViewModel> GetPizzaIngredient();

        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);

        void SavePizzasToWordFile(ReportBindingModel model);

        void SavePizzaIngredientToExcelFile(ReportBindingModel model);

        void SaveOrdersToPdfFile(ReportBindingModel model);
    }
}
