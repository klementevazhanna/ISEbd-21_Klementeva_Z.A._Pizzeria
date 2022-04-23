﻿using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [DataMember]
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [DataMember]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [DataMember]
        [DisplayName("Текст")]
        public string Body { get; set; }
    }
}
