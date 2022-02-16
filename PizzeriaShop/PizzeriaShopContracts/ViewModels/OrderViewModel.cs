using PizzeriaShopContracts.Enums;
using System;
using System.ComponentModel;

namespace PizzeriaShopContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }

        [DisplayName("Пицца")]
        public string PizzaName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
