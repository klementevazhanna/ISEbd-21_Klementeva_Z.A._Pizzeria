using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.BusinessLogics
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly IMessageInfoStorage _messageInfoStorage;

        private readonly IClientStorage _clientStorage;

        public MessageInfoLogic(IMessageInfoStorage messageInfoStorage, IClientStorage clientStorage)
        {
            _messageInfoStorage = messageInfoStorage;
            _clientStorage = clientStorage;
        }

        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return _messageInfoStorage.GetFullList();
            }
            return _messageInfoStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MessageInfoBindingModel model)
        {
            var client = _clientStorage.GetElement(new ClientBindingModel { Email = model.FromMailAddress });
            model.ClientId = client?.Id;

            var element = _messageInfoStorage.GetElement(new MessageInfoBindingModel { MessageId = model.MessageId });
            if (element != null)
            {
                _messageInfoStorage.Update(model);
            }
            else
            {
                model.HasBeenRead = false;
                model.Response = "";

                _messageInfoStorage.Insert(model);
            }
        }
    }
}
