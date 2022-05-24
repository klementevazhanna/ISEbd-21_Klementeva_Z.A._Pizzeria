using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportPizzaIngredientViewModel> GetPizzaIngredient();

        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);

        List<ReportOrdersByDateViewModel> GetOrdersByDate();

        List<ReportWareHouseIngredientViewModel> GetWareHouseIngredient();

        void SaveWareHouseIngredientToExcelFile(ReportBindingModel model);

        void SavePizzasToWordFile(ReportBindingModel model);

        void SavePizzaIngredientToExcelFile(ReportBindingModel model);

        void SaveOrdersToPdfFile(ReportBindingModel model);

        void SaveOrdersByDateToPdfFile(ReportBindingModel model);

        void SaveWareHousesToWordFile(ReportBindingModel model);
    }
}
