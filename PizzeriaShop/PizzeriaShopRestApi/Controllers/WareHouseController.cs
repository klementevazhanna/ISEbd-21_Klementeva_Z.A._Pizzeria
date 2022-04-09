using Microsoft.AspNetCore.Mvc;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WareHouseController : Controller
    {
        private readonly IWareHouseLogic _wareHouse;

        private readonly IIngredientLogic _ingredient;

        public WareHouseController(IWareHouseLogic wareHouse, IIngredientLogic ingredient)
        {
            _wareHouse = wareHouse;
            _ingredient = ingredient;
        }

        [HttpGet]
        public List<WareHouseViewModel> GetWareHouses() => _wareHouse.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateWareHouse(WareHouseBindingModel model) => _wareHouse.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWareHouse(WareHouseBindingModel model) => _wareHouse.Delete(model);

        [HttpPost]
        public void AddIngredient(WareHouseReplenishmentBindingModel model) => _wareHouse.ReplenishByComponent(model);

        [HttpGet]
        public WareHouseViewModel GetWareHouse(int wareHouseId) => _wareHouse.Read(new WareHouseBindingModel { Id = wareHouseId })?[0];

        [HttpGet]
        public List<IngredientViewModel> GetIngredients() => _ingredient.Read(null);
    }
}
