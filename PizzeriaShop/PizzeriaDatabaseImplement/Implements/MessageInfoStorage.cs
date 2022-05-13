using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.StoragesContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new PizzeriaShopDatabase();
            return context.Messages
                .Select(CreateModel)
                .ToList();
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new PizzeriaShopDatabase();
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                return context.Messages
                    .Skip((int)model.ToSkip)
                    .Take((int)model.ToTake)
                    .Select(CreateModel)
                    .ToList();
            }
            return context.Messages
                .Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date) ||
                (model.MessageId != "" && rec.MessageId == model.MessageId))
                .Skip(model.ToSkip ?? 0)
                .Take(model.ToTake ?? context.Messages.Count())
                .Select(CreateModel)
                .ToList();
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new PizzeriaShopDatabase();
            MessageInfo message = context.Messages
                .Include(x => x.Client)
                .FirstOrDefault(rec => rec.MessageId == model.MessageId);
            return message != null ? CreateModel(message) : null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new PizzeriaShopDatabase();
            context.Messages.Add(CreateModel(model, new MessageInfo()));
            context.SaveChanges();
        }

        public void Update(MessageInfoBindingModel model)
        {
            using var context = new PizzeriaShopDatabase();
            MessageInfo message = context.Messages.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (message == null)
            {
                throw new Exception("Письмо не найдено");
            }
            CreateModel(model, message);
            context.SaveChanges();
        }

        private MessageInfoViewModel CreateModel(MessageInfo model)
        {
            return new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.SenderName,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                HasBeenRead = model.HasBeenRead ? "Да" : "Нет",
                Response = model.Response
            };
        }

        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo message)
        {
            message.MessageId = model.MessageId;
            message.ClientId = model.ClientId;
            message.SenderName = model.FromMailAddress;
            message.DateDelivery = model.DateDelivery;
            message.Subject = model.Subject;
            message.Body = model.Body;
            message.HasBeenRead = model.HasBeenRead;
            message.Response = model.Response;
            return message;
        }
    }
}
