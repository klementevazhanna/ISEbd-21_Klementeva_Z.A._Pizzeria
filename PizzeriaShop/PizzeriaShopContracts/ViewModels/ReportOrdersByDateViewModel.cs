﻿using System;

namespace PizzeriaShopContracts.ViewModels
{
    public class ReportOrdersByDateViewModel
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
