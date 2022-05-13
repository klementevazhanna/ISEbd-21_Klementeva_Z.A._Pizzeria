using PizzeriaShopBusinessLogic.MailWorker;
using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.BusinessLogicsContracts;
using PizzeriaShopContracts.ViewModels;
using System;
using System.Windows.Forms;

namespace PizzeriaShopView
{
    public partial class FormMessage : Form
    {
        public string MessageId
        {
            set { _messageId = value; }
        }

        private readonly IMessageInfoLogic _messageLogic;

        private readonly IClientLogic _clientLogic;

        private readonly AbstractMailWorker _mailWorker;

        private string _messageId;

        public FormMessage(IMessageInfoLogic messageLogic, IClientLogic clientLogic, AbstractMailWorker mailWorker)
        {
            InitializeComponent();
            _messageLogic = messageLogic;
            _clientLogic = clientLogic;
            _mailWorker = mailWorker;
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {
            if (_messageId != null)
            {
                try
                {
                    MessageInfoViewModel view = _messageLogic.Read(new MessageInfoBindingModel { MessageId = _messageId })?[0];
                    if (view != null)
                    {
                        if (view.HasBeenRead == "Нет")
                        {
                            _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                            {
                                ClientId = _clientLogic.Read(new ClientBindingModel { Email = view.SenderName })?[0].Id,
                                MessageId = _messageId,
                                FromMailAddress = view.SenderName,
                                Subject = view.Subject,
                                Body = view.Body,
                                DateDelivery = view.DateDelivery,
                                HasBeenRead = true,
                                Response = view.Response
                            });
                        }

                        textBoxSubject.Text = view.Subject;
                        textBoxSenderName.Text = view.SenderName;
                        textBoxDateDelivery.Text = view.DateDelivery.ToString();
                        textBoxBody.Text = view.Body;
                        textBoxResponse.Text = view.Response;

                        if (view.Response != "")
                        {
                            buttonMakeResponse.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonMakeResponse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxResponse.Text))
            {
                MessageBox.Show("Заполните ответ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _mailWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = textBoxSenderName.Text,
                    Subject = "Ответ на: " + textBoxSubject.Text,
                    Text = textBoxResponse.Text
                });

                _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                {
                    ClientId = _clientLogic.Read(new ClientBindingModel { Email = textBoxSenderName.Text })?[0].Id,
                    MessageId = _messageId,
                    FromMailAddress = textBoxSenderName.Text,
                    Subject = textBoxSubject.Text,
                    Body = textBoxBody.Text,
                    DateDelivery = DateTime.Parse(textBoxDateDelivery.Text),
                    HasBeenRead = true,
                    Response = textBoxResponse.Text
                });

                MessageBox.Show("Ответ отправлен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
