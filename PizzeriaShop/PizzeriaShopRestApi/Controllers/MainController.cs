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
    public class MainController : Controller
    {
        private readonly IOrderLogic _order;

        private readonly IPizzaLogic _pizza;

        public MainController(IOrderLogic order, IPizzaLogic pizza)
        {
            _order = order;
            _pizza = pizza;
        }

        [HttpGet]
        public List<PizzaViewModel> GetPizzaList() => _pizza.Read(null)?.ToList();

        [HttpGet]
        public PizzaViewModel GetPizza(int pizzaId) => _pizza.Read(new PizzaBindingModel { Id = pizzaId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
    }
}
