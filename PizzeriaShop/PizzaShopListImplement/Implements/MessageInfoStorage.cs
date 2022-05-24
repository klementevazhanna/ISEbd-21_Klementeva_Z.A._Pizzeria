using PizzaShopListImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace PizzaShopListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton _source;

        public MessageInfoStorage()
        {
            _source = DataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var message in _source.Messages)
            {
                result.Add(CreateModel(message));
            }
            return result;
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();

            int toSkip = model.ToSkip ?? 0;
            int toTake = model.ToTake ?? _source.Messages.Count;
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                foreach (var message in _source.Messages)
                {
                    if (toSkip > 0)
                    {
                        toSkip--;
                        continue;
                    }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(message));
                        toTake--;
                    }
                }
                return result;
            }

            foreach (var message in _source.Messages)
            {
                if ((model.ClientId.HasValue && message.ClientId == model.ClientId) ||
                (!model.ClientId.HasValue && message.DateDelivery.Date == model.DateDelivery.Date))
                {
                    if (toSkip > 0)
                    {
                        toSkip--;
                        continue;
                    }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(message));
                        toTake--;
                    }
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var message in _source.Messages)
            {
                if (message.MessageId == model.MessageId)
                {
                    return CreateModel(message);
                }
            }
            return null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            _source.Messages.Add(CreateModel(model, new MessageInfo()));
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo tmpMessage = null;
            foreach (var message in _source.Messages)
            {
                if (message.MessageId == model.MessageId)
                {
                    tmpMessage = message;
                }
            }
            if (tmpMessage == null)
            {
                throw new Exception("Письмо не найдено");
            }
            CreateModel(model, tmpMessage);
        }

        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo message)
        {
            string clientName = string.Empty;
            foreach (var client in _source.Clients)
            {

                if (client.Id == model.ClientId)
                {
                    clientName = client.ClientFIO;
                    break;
                }
            }
            message.MessageId = model.MessageId;
            message.SenderName = clientName;
            message.DateDelivery = model.DateDelivery;
            message.Subject = model.Subject;
            message.Body = model.Body;
            message.HasBeenRead = model.HasBeenRead;
            message.Response = model.Response;
            return message;
        }

        private MessageInfoViewModel CreateModel(MessageInfo message)
        {
            return new MessageInfoViewModel
            {
                MessageId = message.MessageId,
                SenderName = message.SenderName,
                DateDelivery = message.DateDelivery,
                Subject = message.Subject,
                Body = message.Body,
                HasBeenRead = message.HasBeenRead ? "Да" : "Нет",
                Response = message.Response
            };
        }
    }
}
