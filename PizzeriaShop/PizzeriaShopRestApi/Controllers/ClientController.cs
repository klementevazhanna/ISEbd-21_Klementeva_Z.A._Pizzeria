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
    public class ClientController : Controller
    {
        private readonly IClientLogic _clientLogic;

        private readonly IMessageInfoLogic _messageLogic;

        private readonly IMessageInfoLogic _mailLogic;

        private readonly int _mailsOnPage = 3;

        public ClientController(IClientLogic logic, IMessageInfoLogic messageLogic, IMessageInfoLogic mailLogic)
        {
            _clientLogic = logic;
            _messageLogic = messageLogic;
            _mailLogic = mailLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _clientLogic.Read(new ClientBindingModel { Email = login, Password = password });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpGet]
        public (List<MessageInfoViewModel>, bool) GetMessages(int clientId, int page)
        {
            var list = _mailLogic.Read(new MessageInfoBindingModel
            {
                ClientId = clientId,
                ToSkip = (page - 1) * _mailsOnPage,
                ToTake = _mailsOnPage + 1
            }).ToList();
            var hasNext = !(list.Count <= _mailsOnPage);
            return (list.Take(_mailsOnPage).ToList(), hasNext);
        }

        [HttpPost]
        public void Register(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _clientLogic.CreateOrUpdate(model);
    }
}
