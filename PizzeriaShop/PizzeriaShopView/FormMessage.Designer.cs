
namespace PizzeriaShopView
{
    partial class FormMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.labelBody = new System.Windows.Forms.Label();
            this.buttonMakeResponse = new System.Windows.Forms.Button();
            this.textBoxResponse = new System.Windows.Forms.TextBox();
            this.labelResponse = new System.Windows.Forms.Label();
            this.textBoxDateDelivery = new System.Windows.Forms.TextBox();
            this.labelDateDelivery = new System.Windows.Forms.Label();
            this.textBoxSenderName = new System.Windows.Forms.TextBox();
            this.labelSenderName = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(357, 453);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 33);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(17, 130);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.ReadOnly = true;
            this.textBoxBody.Size = new System.Drawing.Size(442, 147);
            this.textBoxBody.TabIndex = 22;
            // 
            // labelBody
            // 
            this.labelBody.AutoSize = true;
            this.labelBody.Location = new System.Drawing.Point(12, 105);
            this.labelBody.Name = "labelBody";
            this.labelBody.Size = new System.Drawing.Size(94, 17);
            this.labelBody.TabIndex = 21;
            this.labelBody.Text = "Содержание:";
            // 
            // buttonMakeResponse
            // 
            this.buttonMakeResponse.Location = new System.Drawing.Point(17, 453);
            this.buttonMakeResponse.Name = "buttonMakeResponse";
            this.buttonMakeResponse.Size = new System.Drawing.Size(102, 33);
            this.buttonMakeResponse.TabIndex = 20;
            this.buttonMakeResponse.Text = "Ответить";
            this.buttonMakeResponse.UseVisualStyleBackColor = true;
            this.buttonMakeResponse.Click += new System.EventHandler(this.buttonMakeResponse_Click);
            // 
            // textBoxResponse
            // 
            this.textBoxResponse.Location = new System.Drawing.Point(17, 308);
            this.textBoxResponse.Multiline = true;
            this.textBoxResponse.Name = "textBoxResponse";
            this.textBoxResponse.Size = new System.Drawing.Size(442, 130);
            this.textBoxResponse.TabIndex = 19;
            // 
            // labelResponse
            // 
            this.labelResponse.AutoSize = true;
            this.labelResponse.Location = new System.Drawing.Point(13, 284);
            this.labelResponse.Name = "labelResponse";
            this.labelResponse.Size = new System.Drawing.Size(52, 17);
            this.labelResponse.TabIndex = 18;
            this.labelResponse.Text = "Ответ:";
            // 
            // textBoxDateDelivery
            // 
            this.textBoxDateDelivery.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxDateDelivery.Location = new System.Drawing.Point(117, 71);
            this.textBoxDateDelivery.Name = "textBoxDateDelivery";
            this.textBoxDateDelivery.ReadOnly = true;
            this.textBoxDateDelivery.Size = new System.Drawing.Size(342, 22);
            this.textBoxDateDelivery.TabIndex = 17;
            // 
            // labelDateDelivery
            // 
            this.labelDateDelivery.AutoSize = true;
            this.labelDateDelivery.Location = new System.Drawing.Point(12, 74);
            this.labelDateDelivery.Name = "labelDateDelivery";
            this.labelDateDelivery.Size = new System.Drawing.Size(97, 17);
            this.labelDateDelivery.TabIndex = 16;
            this.labelDateDelivery.Text = "Дата письма:";
            // 
            // textBoxSenderName
            // 
            this.textBoxSenderName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSenderName.Location = new System.Drawing.Point(117, 38);
            this.textBoxSenderName.Name = "textBoxSenderName";
            this.textBoxSenderName.ReadOnly = true;
            this.textBoxSenderName.Size = new System.Drawing.Size(342, 22);
            this.textBoxSenderName.TabIndex = 15;
            // 
            // labelSenderName
            // 
            this.labelSenderName.AutoSize = true;
            this.labelSenderName.Location = new System.Drawing.Point(12, 41);
            this.labelSenderName.Name = "labelSenderName";
            this.labelSenderName.Size = new System.Drawing.Size(99, 17);
            this.labelSenderName.TabIndex = 14;
            this.labelSenderName.Text = "Отправитель:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(117, 6);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.ReadOnly = true;
            this.textBoxSubject.Size = new System.Drawing.Size(342, 22);
            this.textBoxSubject.TabIndex = 13;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(12, 9);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(80, 17);
            this.labelSubject.TabIndex = 12;
            this.labelSubject.Text = "Заголовок:";
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 497);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.labelBody);
            this.Controls.Add(this.buttonMakeResponse);
            this.Controls.Add(this.textBoxResponse);
            this.Controls.Add(this.labelResponse);
            this.Controls.Add(this.textBoxDateDelivery);
            this.Controls.Add(this.labelDateDelivery);
            this.Controls.Add(this.textBoxSenderName);
            this.Controls.Add(this.labelSenderName);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.labelSubject);
            this.Name = "FormMessage";
            this.Text = "Письмо";
            this.Load += new System.EventHandler(this.FormMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.Label labelBody;
        private System.Windows.Forms.Button buttonMakeResponse;
        private System.Windows.Forms.TextBox textBoxResponse;
        private System.Windows.Forms.Label labelResponse;
        private System.Windows.Forms.TextBox textBoxDateDelivery;
        private System.Windows.Forms.Label labelDateDelivery;
        private System.Windows.Forms.TextBox textBoxSenderName;
        private System.Windows.Forms.Label labelSenderName;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Label labelSubject;
    }
}