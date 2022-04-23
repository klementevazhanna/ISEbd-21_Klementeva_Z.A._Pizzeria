using Microsoft.AspNetCore.Mvc;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _clientLogic;

        private readonly IMessageInfoLogic _messageLogic;

        public ClientController(IClientLogic logic, IMessageInfoLogic messageLogic)
        {
            _clientLogic = logic;
            _messageLogic = messageLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _clientLogic.Read(new ClientBindingModel { Email = login, Password = password });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);

        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) => _messageLogic.Read(new MessageInfoBindingModel { ClientId = clientId });
    }
}
