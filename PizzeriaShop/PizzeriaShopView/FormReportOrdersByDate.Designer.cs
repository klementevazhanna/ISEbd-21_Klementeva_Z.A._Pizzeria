
namespace PizzeriaShopView
{
    partial class FormReportOrdersByDate
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
            this.buttonForm = new System.Windows.Forms.Button();
            this.buttonFormPdf = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(474, 3);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(124, 40);
            this.buttonForm.TabIndex = 0;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // buttonFormPdf
            // 
            this.buttonFormPdf.Location = new System.Drawing.Point(604, 3);
            this.buttonFormPdf.Name = "buttonFormPdf";
            this.buttonFormPdf.Size = new System.Drawing.Size(169, 40);
            this.buttonFormPdf.TabIndex = 1;
            this.buttonFormPdf.Text = "Сформировать в PDF";
            this.buttonFormPdf.UseVisualStyleBackColor = true;
            this.buttonFormPdf.Click += new System.EventHandler(this.buttonFormPdf_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.buttonForm);
            this.panel.Controls.Add(this.buttonFormPdf);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 47);
            this.panel.TabIndex = 2;
            // 
            // FormReportOrdersByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Name = "FormReportOrdersByDate";
            this.Text = "Заказы по датам";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Button buttonFormPdf;
        private System.Windows.Forms.Panel panel;
    }
}