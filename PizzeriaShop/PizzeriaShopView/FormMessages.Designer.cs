
namespace PizzeriaShopView
{
    partial class FormMessages
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCheckMessage = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.labelPageNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(904, 147);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonCheckMessage
            // 
            this.buttonCheckMessage.Location = new System.Drawing.Point(787, 174);
            this.buttonCheckMessage.Name = "buttonCheckMessage";
            this.buttonCheckMessage.Size = new System.Drawing.Size(129, 38);
            this.buttonCheckMessage.TabIndex = 9;
            this.buttonCheckMessage.Text = "Открыть";
            this.buttonCheckMessage.UseVisualStyleBackColor = true;
            this.buttonCheckMessage.Click += new System.EventHandler(this.buttonCheckMessage_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(529, 174);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(129, 38);
            this.buttonNext.TabIndex = 8;
            this.buttonNext.Text = "Далее";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(13, 174);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(129, 38);
            this.buttonPrev.TabIndex = 7;
            this.buttonPrev.Text = "Назад";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // labelPageNum
            // 
            this.labelPageNum.AutoSize = true;
            this.labelPageNum.Location = new System.Drawing.Point(232, 182);
            this.labelPageNum.Name = "labelPageNum";
            this.labelPageNum.Size = new System.Drawing.Size(0, 17);
            this.labelPageNum.TabIndex = 10;
            // 
            // FormMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 228);
            this.Controls.Add(this.labelPageNum);
            this.Controls.Add(this.buttonCheckMessage);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormMessages";
            this.Text = "Письма";
            this.Load += new System.EventHandler(this.FormMessages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCheckMessage;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Label labelPageNum;
    }
}