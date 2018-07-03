namespace WeatherCrawler
{
    partial class FormOption
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.textBoxCollectionName = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxPw = new System.Windows.Forms.TextBox();
            this.buttonOptionOk = new System.Windows.Forms.Button();
            this.buttonOptionCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "수집서버 DB 정보";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP 주소";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "DB 명";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Collection 명";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "PW";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(140, 71);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(100, 21);
            this.textBoxIpAddress.TabIndex = 6;
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(140, 98);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(100, 21);
            this.textBoxDBName.TabIndex = 7;
            // 
            // textBoxCollectionName
            // 
            this.textBoxCollectionName.Location = new System.Drawing.Point(140, 126);
            this.textBoxCollectionName.Name = "textBoxCollectionName";
            this.textBoxCollectionName.Size = new System.Drawing.Size(100, 21);
            this.textBoxCollectionName.TabIndex = 8;
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(140, 152);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(100, 21);
            this.textBoxId.TabIndex = 9;
            // 
            // textBoxPw
            // 
            this.textBoxPw.Location = new System.Drawing.Point(140, 181);
            this.textBoxPw.Name = "textBoxPw";
            this.textBoxPw.Size = new System.Drawing.Size(100, 21);
            this.textBoxPw.TabIndex = 10;
            // 
            // buttonOptionOk
            // 
            this.buttonOptionOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.buttonOptionOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOptionOk.FlatAppearance.BorderSize = 0;
            this.buttonOptionOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOptionOk.ForeColor = System.Drawing.Color.White;
            this.buttonOptionOk.Location = new System.Drawing.Point(76, 319);
            this.buttonOptionOk.Name = "buttonOptionOk";
            this.buttonOptionOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOptionOk.TabIndex = 11;
            this.buttonOptionOk.Text = "확인";
            this.buttonOptionOk.UseVisualStyleBackColor = false;
            this.buttonOptionOk.Click += new System.EventHandler(this.buttonOptionOk_Click);
            // 
            // buttonOptionCancel
            // 
            this.buttonOptionCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.buttonOptionCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOptionCancel.FlatAppearance.BorderSize = 0;
            this.buttonOptionCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOptionCancel.Location = new System.Drawing.Point(210, 320);
            this.buttonOptionCancel.Name = "buttonOptionCancel";
            this.buttonOptionCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonOptionCancel.TabIndex = 12;
            this.buttonOptionCancel.Text = "취소";
            this.buttonOptionCancel.UseVisualStyleBackColor = false;
            this.buttonOptionCancel.Click += new System.EventHandler(this.buttonOptionCancel_Click);
            // 
            // FormOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.buttonOptionCancel);
            this.Controls.Add(this.buttonOptionOk);
            this.Controls.Add(this.textBoxPw);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.textBoxCollectionName);
            this.Controls.Add(this.textBoxDBName);
            this.Controls.Add(this.textBoxIpAddress);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormOption";
            this.Text = "옵션";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.TextBox textBoxCollectionName;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxPw;
        private System.Windows.Forms.Button buttonOptionOk;
        private System.Windows.Forms.Button buttonOptionCancel;
    }
}