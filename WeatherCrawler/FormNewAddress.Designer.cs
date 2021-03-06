﻿namespace WeatherCrawler
{
    partial class FormNewAddress
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
            this.textBoxNewAddress = new System.Windows.Forms.TextBox();
            this.buttonFindAddress = new System.Windows.Forms.Button();
            this.buttonAddAddress = new System.Windows.Forms.Button();
            this.buttonCancelAddAddress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "새로운 주소 추가";
            // 
            // textBoxNewAddress
            // 
            this.textBoxNewAddress.Location = new System.Drawing.Point(23, 55);
            this.textBoxNewAddress.Name = "textBoxNewAddress";
            this.textBoxNewAddress.Size = new System.Drawing.Size(200, 21);
            this.textBoxNewAddress.TabIndex = 1;
            // 
            // buttonFindAddress
            // 
            this.buttonFindAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.buttonFindAddress.FlatAppearance.BorderSize = 0;
            this.buttonFindAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFindAddress.Location = new System.Drawing.Point(250, 55);
            this.buttonFindAddress.Name = "buttonFindAddress";
            this.buttonFindAddress.Size = new System.Drawing.Size(75, 23);
            this.buttonFindAddress.TabIndex = 2;
            this.buttonFindAddress.Text = "주소찾기";
            this.buttonFindAddress.UseVisualStyleBackColor = false;
            this.buttonFindAddress.Click += new System.EventHandler(this.buttonFindAddress_Click);
            // 
            // buttonAddAddress
            // 
            this.buttonAddAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.buttonAddAddress.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAddAddress.FlatAppearance.BorderSize = 0;
            this.buttonAddAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddAddress.Location = new System.Drawing.Point(96, 124);
            this.buttonAddAddress.Name = "buttonAddAddress";
            this.buttonAddAddress.Size = new System.Drawing.Size(75, 23);
            this.buttonAddAddress.TabIndex = 3;
            this.buttonAddAddress.Text = "추가";
            this.buttonAddAddress.UseVisualStyleBackColor = false;
            this.buttonAddAddress.Click += new System.EventHandler(this.buttonAddAddress_Click);
            // 
            // buttonCancelAddAddress
            // 
            this.buttonCancelAddAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.buttonCancelAddAddress.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelAddAddress.FlatAppearance.BorderSize = 0;
            this.buttonCancelAddAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelAddAddress.Location = new System.Drawing.Point(204, 126);
            this.buttonCancelAddAddress.Name = "buttonCancelAddAddress";
            this.buttonCancelAddAddress.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelAddAddress.TabIndex = 4;
            this.buttonCancelAddAddress.Text = "취소";
            this.buttonCancelAddAddress.UseVisualStyleBackColor = false;
            this.buttonCancelAddAddress.Click += new System.EventHandler(this.buttonCancelAddAddress_Click);
            // 
            // FormNewAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.buttonCancelAddAddress);
            this.Controls.Add(this.buttonAddAddress);
            this.Controls.Add(this.buttonFindAddress);
            this.Controls.Add(this.textBoxNewAddress);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormNewAddress";
            this.Text = "새로운 주소";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNewAddress;
        private System.Windows.Forms.Button buttonFindAddress;
        private System.Windows.Forms.Button buttonAddAddress;
        private System.Windows.Forms.Button buttonCancelAddAddress;
    }
}