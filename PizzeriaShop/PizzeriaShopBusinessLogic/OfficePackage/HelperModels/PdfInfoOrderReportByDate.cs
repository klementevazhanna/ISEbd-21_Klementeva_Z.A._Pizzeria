using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoOrderReportByDate
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportOrdersByDateViewModel> Orders { get; set; }
    }
}
