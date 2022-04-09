using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace PizzeriaShopView
{
    public partial class FormReplenishmentWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IWareHouseLogic _logicW;

        private readonly IIngredientLogic _logicI;

        public FormReplenishmentWareHouse(IIngredientLogic logicI, IWareHouseLogic logicW)
        {
            InitializeComponent();
            _logicW = logicW;
            _logicI = logicI;
        }

        private void FormReplenishmentWareHouse_Load(object sender, EventArgs e)
        {
            try
            {
                List<WareHouseViewModel> listW = _logicW.Read(null);
                if (listW != null)
                {
                    comboBoxWareHouse.DisplayMember = "WareHouseName";
                    comboBoxWareHouse.ValueMember = "Id";
                    comboBoxWareHouse.DataSource = listW;
                    comboBoxWareHouse.SelectedItem = null;
                }
                else
                {
                    throw new Exception("Не удалось загрузить список складов");
                }

                List<IngredientViewModel> listI = _logicI.Read(null);
                if (listI != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = listI;
                    comboBoxIngredient.SelectedItem = null;
                }
                else
                {
                    throw new Exception("Не удалось загрузить список ингредиентов");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите продукт", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWareHouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<IngredientViewModel> listI = _logicI.Read(null);
                _logicW.ReplenishByComponent(new WareHouseReplenishmentBindingModel
                {
                    WareHouseId = Convert.ToInt32(comboBoxWareHouse.SelectedValue),
                    IngredientId = listI[comboBoxIngredient.SelectedIndex].Id,
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
