using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace PizzeriaShopView
{
    public partial class FormMessages : Form
    {
        private readonly IMessageInfoLogic _logic;

        private bool _hasNext = false;

        private readonly int _mailsOnPage = 3;

        private int _currentPage = 0;

        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            if (_mailsOnPage < 1)
            {
                _mailsOnPage = 4;
            }
        }

        private void FormMessages_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(new MessageInfoBindingModel
                {
                    ToSkip = _currentPage * _mailsOnPage,
                    ToTake = _mailsOnPage + 1
                });
                _hasNext = !(list.Count <= _mailsOnPage);
                if (_hasNext)
                {
                    buttonNext.Text = $"Далее ({(_currentPage + 2)})";
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Text = "Далее";
                    buttonNext.Enabled = false;
                }
                if (_currentPage == 0)
                {
                    buttonPrev.Enabled = false;
                }
                labelPageNum.Text = $"Текущая страница: {(_currentPage + 1)}";
                Program.ConfigGrid(list.Take(_mailsOnPage).ToList(), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (_hasNext)
            {
                _currentPage++;
                buttonPrev.Enabled = true;
                buttonPrev.Text = $"Назад ({_currentPage})";
                LoadData();
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if ((_currentPage - 1) >= 0)
            {
                _currentPage--;
                buttonNext.Enabled = true;
                buttonNext.Text = $"Далее ({(_currentPage + 2)})";
                if (_currentPage == 0)
                {
                    buttonPrev.Enabled = false;
                    buttonPrev.Text = "Назад";
                }
                else
                {
                    buttonPrev.Text = $"Назад ({_currentPage})";
                }
                LoadData();
            }
        }

        private void buttonCheckMessage_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormMessage>();
                form.MessageId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
