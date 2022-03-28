using PizzeriaShopBusinessLogic.OfficePackage;
using PizzeriaShopBusinessLogic.OfficePackage.HelperModels;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaShopBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IPizzaStorage _pizzaStorage;

        private readonly IOrderStorage _orderStorage;

        private readonly IWareHouseStorage _wareHouseStorage;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IPizzaStorage pizzaStorage, IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _pizzaStorage = pizzaStorage;
            _orderStorage = orderStorage;
            _wareHouseStorage = wareHouseStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<ReportPizzaIngredientViewModel> GetPizzaIngredient()
        {
            var pizzas = _pizzaStorage.GetFullList();
            var list = new List<ReportPizzaIngredientViewModel>();
            foreach (var pizza in pizzas)
            {
                var record = new ReportPizzaIngredientViewModel
                {
                    PizzaName = pizza.PizzaName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in pizza.PizzaIngredients)
                {
                    record.Ingredients.Add(new Tuple<string, int>(ingredient.Value.Item1, ingredient.Value.Item2));
                    record.TotalCount += ingredient.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                PizzaName = x.PizzaName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        public List<ReportOrdersByDateViewModel> GetOrdersByDate()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new ReportOrdersByDateViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }

        public List<ReportWareHouseIngredientViewModel> GetWareHouseIngredient()
        {
            var wareHouses = _wareHouseStorage.GetFullList();
            var list = new List<ReportWareHouseIngredientViewModel>();
            foreach (var wareHouse in wareHouses)
            {
                var record = new ReportWareHouseIngredientViewModel
                {
                    WareHouseName = wareHouse.WareHouseName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in wareHouse.WareHouseIngredients)
                {
                    record.Ingredients.Add(new Tuple<string, int>(ingredient.Value.Item1, ingredient.Value.Item2));
                    record.TotalCount += ingredient.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public void SavePizzasToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = _pizzaStorage.GetFullList()
            });
        }

        public void SaveWareHousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateWareHousesDoc(new WordInfoWareHouses
            {
                FileName = model.FileName,
                Title = "Список складов",
                WareHouses = _wareHouseStorage.GetFullList()
            });
        }

        public void SavePizzaIngredientToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                PizzaIngredients = GetPizzaIngredient()
            });
        }

        public void SaveWareHouseIngredientToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReportWareHouses(new ExcelInfoWareHouses
            {
                FileName = model.FileName,
                Title = "Список складов",
                WareHouseIngredients = GetWareHouseIngredient()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public void SaveOrdersByDateToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocOrderReportByDate(new PdfInfoOrderReportByDate
            {
                FileName = model.FileName,
                Title = "Список заказов за весь период",
                Orders = GetOrdersByDate()
            });
        }
    }
}
