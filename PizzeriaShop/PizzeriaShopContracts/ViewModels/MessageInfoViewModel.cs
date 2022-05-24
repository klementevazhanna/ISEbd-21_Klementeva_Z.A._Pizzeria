﻿using PizzeriaShopContracts.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 100)]
        [DataMember]
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [Column(title: "Дата", width: 50)]
        [DataMember]
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 150)]
        [DataMember]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("Текст")]
        public string Body { get; set; }

        [DataMember]
        [DisplayName("Прочитано")]
        public string HasBeenRead { get; set; }

        [DataMember]
        [DisplayName("Ответ")]
        public string Response { get; set; }
    }
}
