using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using PizzeriaShopWareHouseApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PizzeriaShopWareHouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIWareHouse.GetRequest<List<WareHouseViewModel>>("api/wareHouse/GetWareHouses"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {

            if (!string.IsNullOrEmpty(password))
            {
                if (password != Program.Password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Enter = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string wareHouseName, string responsiblePersonFIO)
        {
            if (!string.IsNullOrEmpty(wareHouseName) && !string.IsNullOrEmpty(responsiblePersonFIO))
            {
                APIWareHouse.PostRequest("api/wareHouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
                {
                    ResponsiblePersonFIO = responsiblePersonFIO,
                    WareHouseName = wareHouseName,
                    DateCreate = DateTime.Now,
                    WareHouseIngredients = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите название и ответственного");
        }

        [HttpGet]
        public IActionResult Update(int wareHouseId)
        {
            WareHouseViewModel wareHouse = APIWareHouse.GetRequest<WareHouseViewModel>($"api/wareHouse/GetWareHouse?wareHouseId={wareHouseId}");
            ViewBag.Ingredients = wareHouse.WareHouseIngredients.Values;
            ViewBag.WareHouseName = wareHouse.WareHouseName;
            ViewBag.ResponsiblePersonFIO = wareHouse.ResponsiblePersonFIO;
            return View();
        }

        [HttpPost]
        public void Update(int wareHouseId, string wareHouseName, string responsiblePersonFIO)
        {
            if (!string.IsNullOrEmpty(wareHouseName) && !string.IsNullOrEmpty(responsiblePersonFIO))
            {
                var wareHouse = APIWareHouse.GetRequest<WareHouseViewModel>($"api/wareHouse/GetWareHouse?wareHouseId={wareHouseId}");
                if (wareHouse == null)
                {
                    return;
                }
                APIWareHouse.PostRequest("api/wareHouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
                {
                    ResponsiblePersonFIO = responsiblePersonFIO,
                    WareHouseName = wareHouseName,
                    DateCreate = DateTime.Now,
                    WareHouseIngredients = wareHouse.WareHouseIngredients,
                    Id = wareHouse.Id
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.WareHouse = APIWareHouse.GetRequest<List<WareHouseViewModel>>("api/wareHouse/GetWareHouses");
            return View();
        }

        [HttpPost]
        public void Delete(int wareHouseId)
        {
            APIWareHouse.PostRequest("api/wareHouse/DeleteWareHouse", new WareHouseBindingModel
            {
                Id = wareHouseId
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult AddIngredient()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.WareHouse = APIWareHouse.GetRequest<List<WareHouseViewModel>>("api/wareHouse/GetWareHouses");
            ViewBag.Ingredient = APIWareHouse.GetRequest<List<IngredientViewModel>>("api/wareHouse/GetIngredients");
            return View();
        }

        [HttpPost]
        public void AddIngredient(int wareHouseId, int ingredientId, int count)
        {
            APIWareHouse.PostRequest("api/wareHouse/AddIngredient", new WareHouseReplenishmentBindingModel
            {
                WareHouseId = wareHouseId,
                IngredientId = ingredientId,
                Count = count
            });
            Response.Redirect("AddIngredient");
        }
    }
}
