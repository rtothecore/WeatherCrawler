﻿namespace WeatherCrawler
{
    partial class FormSearchAddress
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearchAddressText = new System.Windows.Forms.TextBox();
            this.buttonSearchAddress = new System.Windows.Forms.Button();
            this.listBoxSeachedAddress = new System.Windows.Forms.ListBox();
            this.buttonSelectAddress = new System.Windows.Forms.Button();
            this.buttonCancelAddress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "주소검색";
            // 
            // textBoxSearchAddressText
            // 
            this.textBoxSearchAddressText.Location = new System.Drawing.Point(30, 60);
            this.textBoxSearchAddressText.Name = "textBoxSearchAddressText";
            this.textBoxSearchAddressText.Size = new System.Drawing.Size(100, 21);
            this.textBoxSearchAddressText.TabIndex = 1;
            // 
            // buttonSearchAddress
            // 
            this.buttonSearchAddress.Location = new System.Drawing.Point(160, 60);
            this.buttonSearchAddress.Name = "buttonSearchAddress";
            this.buttonSearchAddress.Size = new System.Drawing.Size(75, 25);
            this.buttonSearchAddress.TabIndex = 2;
            this.buttonSearchAddress.Text = "검색";
            this.buttonSearchAddress.UseVisualStyleBackColor = true;
            // 
            // listBoxSeachedAddress
            // 
            this.listBoxSeachedAddress.FormattingEnabled = true;
            this.listBoxSeachedAddress.ItemHeight = 12;
            this.listBoxSeachedAddress.Location = new System.Drawing.Point(33, 99);
            this.listBoxSeachedAddress.Name = "listBoxSeachedAddress";
            this.listBoxSeachedAddress.Size = new System.Drawing.Size(325, 172);
            this.listBoxSeachedAddress.TabIndex = 3;
            // 
            // buttonSelectAddress
            // 
            this.buttonSelectAddress.Location = new System.Drawing.Point(93, 325);
            this.buttonSelectAddress.Name = "buttonSelectAddress";
            this.buttonSelectAddress.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAddress.TabIndex = 4;
            this.buttonSelectAddress.Text = "확인";
            this.buttonSelectAddress.UseVisualStyleBackColor = true;
            // 
            // buttonCancelAddress
            // 
            this.buttonCancelAddress.Location = new System.Drawing.Point(209, 326);
            this.buttonCancelAddress.Name = "buttonCancelAddress";
            this.buttonCancelAddress.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelAddress.TabIndex = 5;
            this.buttonCancelAddress.Text = "취소";
            this.buttonCancelAddress.UseVisualStyleBackColor = true;
            this.buttonCancelAddress.Click += new System.EventHandler(this.buttonCancelAddress_Click);
            // 
            // FormSearchAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.buttonCancelAddress);
            this.Controls.Add(this.buttonSelectAddress);
            this.Controls.Add(this.listBoxSeachedAddress);
            this.Controls.Add(this.buttonSearchAddress);
            this.Controls.Add(this.textBoxSearchAddressText);
            this.Controls.Add(this.label1);
            this.Name = "FormSearchAddress";
            this.Text = "주소찾기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearchAddressText;
        private System.Windows.Forms.Button buttonSearchAddress;
        private System.Windows.Forms.ListBox listBoxSeachedAddress;
        private System.Windows.Forms.Button buttonSelectAddress;
        private System.Windows.Forms.Button buttonCancelAddress;
    }
}