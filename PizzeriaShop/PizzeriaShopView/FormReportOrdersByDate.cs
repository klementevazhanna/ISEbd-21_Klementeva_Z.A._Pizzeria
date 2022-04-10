using Microsoft.Reporting.WinForms;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using System;
using System.IO;
using System.Windows.Forms;

namespace PizzeriaShopView
{
    public partial class FormReportOrdersByDate : Form
    {
        private readonly ReportViewer reportViewer;

        private readonly IReportLogic _logic;

        public FormReportOrdersByDate(IReportLogic logic)
        {
            InitializeComponent();
            _logic = logic;

            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill
            };
            reportViewer.LocalReport.LoadReportDefinition(new FileStream("C:\\Users\\f0rge\\VSProjects\\TechProgr\\FourthSemestrLabWorks\\ISEbd-21_Klementeva_Z.A._Pizzeria\\PizzeriaShop\\PizzeriaShopView\\ReportOrdersByDate.rdlc", FileMode.Open));
            Controls.Clear();
            Controls.Add(reportViewer);
            Controls.Add(panel);
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = _logic.GetOrdersByDate();

                var source = new ReportDataSource("DataSetOrdersByDate", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFormPdf_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logic.SaveOrdersByDateToPdfFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
