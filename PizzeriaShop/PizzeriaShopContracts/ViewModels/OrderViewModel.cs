using PizzeriaShopContracts.Attributes;
using PizzeriaShopContracts.Enums;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        public int PizzaId { get; set; }

        [Column(title: "Клиент", width: 150)]
        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

        [Column(title: "Исполнитель", width: 150)]
        [DataMember]
        [DisplayName("Исполнитель")]
        public string ImplementerFIO { get; set; }

        [Column(title: "Пицца", width: 150)]
        [DataMember]
        [DisplayName("Пицца")]
        public string PizzaName { get; set; }

        [Column(title: "Количество", width: 100, alignment: DataGridViewContentAlignment.BottomRight)]
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }

        [Column(title: "Сумма", width: 50, alignment: DataGridViewContentAlignment.BottomRight)]
        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [Column(title: "Статус", width: 100)]
        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [Column(title: "Дата создания", width: 100, dateType: "d M y")]
        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [Column(title: "Дата выполнения", width: 100, dateType: "d M y")]
        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
