﻿using PizzeriaShopBusinessLogic.OfficePackage.HelperEnums;
using PizzeriaShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string>
            {
                "3cm", "6cm", "3cm", "2cm", "3cm"
            });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> {
                    "Дата заказа",
                    "Пицца",
                    "Количество",
                    "Сумма",
                    "Статус"
                },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var order in info.Orders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string>
                    {
                        order.DateCreate.ToShortDateString(),
                        order.PizzaName,
                        order.Count.ToString(),
                        order.Sum.ToString(),
                        order.Status.ToString()
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        protected abstract void CreatePdf(PdfInfo info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfo info);
    }
}
