using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaShopView
{
    public partial class FormReportWareHouseIngredients : Form
    {
        private readonly IReportLogic _logic;

        public FormReportWareHouseIngredients(IReportLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormReportWareHouseIngredients_Load(object sender, EventArgs e)
        {
            try
            {
                MethodInfo method = _logic.GetType().GetMethod("GetWareHouseIngredient");
                var dict = (List<ReportWareHouseIngredientViewModel>)method.Invoke(_logic, new object[] { });
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.WareHouseName, "", "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView.Rows.Add(Array.Empty<object>());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MethodInfo method = _logic.GetType().GetMethod("SaveWareHouseIngredientToExcelFile");
                    var dataSource = method.Invoke(_logic, new object[] { new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    } });
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
