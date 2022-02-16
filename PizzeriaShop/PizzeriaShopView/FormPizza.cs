using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace PizzeriaShopView
{
    public partial class FormPizza : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id
        {
            set { id = value; }
        }

        private readonly IPizzaLogic logic;

        private int? id;

        private Dictionary<int, (string, int)> ingredientPizza;

        public FormPizza(IPizzaLogic service)
        {
            InitializeComponent();
            logic = service;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormPizza_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PizzaViewModel view = logic.Read(new PizzaBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.PizzaName;
                        textBoxPrice.Text = view.Price.ToString();
                        ingredientPizza = view.PizzaIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                ingredientPizza = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (ingredientPizza != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in ingredientPizza)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIngredientPizza>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (ingredientPizza.ContainsKey(form.Id))
                {
                    ingredientPizza[form.Id] = (form.IngredientName, form.Count);
                }
                else
                {
                    ingredientPizza.Add(form.Id, (form.IngredientName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormIngredientPizza>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = ingredientPizza[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ingredientPizza[form.Id] = (form.IngredientName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        ingredientPizza.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (ingredientPizza == null || ingredientPizza.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new PizzaBindingModel
                {
                    Id = id,
                    PizzaName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    PizzaIngredients = ingredientPizza
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}
