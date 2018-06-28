namespace WeatherCrawler
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새로운주소ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.옵션ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.끝내기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.주소ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수집ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.모든주소수집실행ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.모든주소수집정지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도움말ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelRunLastTime = new System.Windows.Forms.Label();
            this.labelRunStatus = new System.Windows.Forms.Label();
            this.buttonRunStatus = new System.Windows.Forms.Button();
            this.labelCrawlOption = new System.Windows.Forms.Label();
            this.labelCrawlTerm = new System.Windows.Forms.Label();
            this.labelCurrentAddr = new System.Windows.Forms.Label();
            this.labelAddr = new System.Windows.Forms.Label();
            this.buttonAddr = new System.Windows.Forms.Button();
            this.buttonCrawlOption = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.contextMenuStripAddress = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripCrawlOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.주소ToolStripMenuItem,
            this.수집ToolStripMenuItem,
            this.도움말ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새로운주소ToolStripMenuItem,
            this.옵션ToolStripMenuItem,
            this.끝내기ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 새로운주소ToolStripMenuItem
            // 
            this.새로운주소ToolStripMenuItem.Name = "새로운주소ToolStripMenuItem";
            this.새로운주소ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.새로운주소ToolStripMenuItem.Text = "새로운 주소";
            this.새로운주소ToolStripMenuItem.Click += new System.EventHandler(this.새로운주소ToolStripMenuItem_Click);
            // 
            // 옵션ToolStripMenuItem
            // 
            this.옵션ToolStripMenuItem.Name = "옵션ToolStripMenuItem";
            this.옵션ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.옵션ToolStripMenuItem.Text = "옵션";
            this.옵션ToolStripMenuItem.Click += new System.EventHandler(this.옵션ToolStripMenuItem_Click);
            // 
            // 끝내기ToolStripMenuItem
            // 
            this.끝내기ToolStripMenuItem.Name = "끝내기ToolStripMenuItem";
            this.끝내기ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.끝내기ToolStripMenuItem.Text = "끝내기";
            // 
            // 주소ToolStripMenuItem
            // 
            this.주소ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.삭제ToolStripMenuItem});
            this.주소ToolStripMenuItem.Name = "주소ToolStripMenuItem";
            this.주소ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.주소ToolStripMenuItem.Text = "주소";
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.삭제ToolStripMenuItem.Text = "삭제";
            // 
            // 수집ToolStripMenuItem
            // 
            this.수집ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.모든주소수집실행ToolStripMenuItem,
            this.모든주소수집정지ToolStripMenuItem});
            this.수집ToolStripMenuItem.Name = "수집ToolStripMenuItem";
            this.수집ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.수집ToolStripMenuItem.Text = "수집";
            // 
            // 모든주소수집실행ToolStripMenuItem
            // 
            this.모든주소수집실행ToolStripMenuItem.Name = "모든주소수집실행ToolStripMenuItem";
            this.모든주소수집실행ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.모든주소수집실행ToolStripMenuItem.Text = "모든주소 수집 실행";
            // 
            // 모든주소수집정지ToolStripMenuItem
            // 
            this.모든주소수집정지ToolStripMenuItem.Name = "모든주소수집정지ToolStripMenuItem";
            this.모든주소수집정지ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.모든주소수집정지ToolStripMenuItem.Text = "모든주소 수집 정지";
            // 
            // 도움말ToolStripMenuItem
            // 
            this.도움말ToolStripMenuItem.Name = "도움말ToolStripMenuItem";
            this.도움말ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.도움말ToolStripMenuItem.Text = "도움말";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.splitContainer1.Panel1.Controls.Add(this.labelRunLastTime);
            this.splitContainer1.Panel1.Controls.Add(this.labelRunStatus);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRunStatus);
            this.splitContainer1.Panel1.Controls.Add(this.labelCrawlOption);
            this.splitContainer1.Panel1.Controls.Add(this.labelCrawlTerm);
            this.splitContainer1.Panel1.Controls.Add(this.labelCurrentAddr);
            this.splitContainer1.Panel1.Controls.Add(this.labelAddr);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAddr);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCrawlOption);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxLog);
            this.splitContainer1.Size = new System.Drawing.Size(800, 426);
            this.splitContainer1.TabIndex = 1;
            // 
            // labelRunLastTime
            // 
            this.labelRunLastTime.AutoSize = true;
            this.labelRunLastTime.BackColor = System.Drawing.Color.Transparent;
            this.labelRunLastTime.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRunLastTime.ForeColor = System.Drawing.Color.White;
            this.labelRunLastTime.Location = new System.Drawing.Point(0, 30);
            this.labelRunLastTime.Name = "labelRunLastTime";
            this.labelRunLastTime.Size = new System.Drawing.Size(139, 15);
            this.labelRunLastTime.TabIndex = 10;
            this.labelRunLastTime.Text = "마지막 수집시간 - 13:00";
            this.labelRunLastTime.Click += new System.EventHandler(this.labelRunLastTime_Click);
            // 
            // labelRunStatus
            // 
            this.labelRunStatus.AutoSize = true;
            this.labelRunStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelRunStatus.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRunStatus.ForeColor = System.Drawing.Color.White;
            this.labelRunStatus.Location = new System.Drawing.Point(0, 5);
            this.labelRunStatus.Name = "labelRunStatus";
            this.labelRunStatus.Size = new System.Drawing.Size(58, 21);
            this.labelRunStatus.TabIndex = 9;
            this.labelRunStatus.Text = "수집중";
            this.labelRunStatus.Click += new System.EventHandler(this.labelRunStatus_Click);
            // 
            // buttonRunStatus
            // 
            this.buttonRunStatus.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonRunStatus.FlatAppearance.BorderSize = 0;
            this.buttonRunStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunStatus.Location = new System.Drawing.Point(360, 0);
            this.buttonRunStatus.Name = "buttonRunStatus";
            this.buttonRunStatus.Size = new System.Drawing.Size(150, 50);
            this.buttonRunStatus.TabIndex = 8;
            this.buttonRunStatus.TabStop = false;
            this.buttonRunStatus.UseVisualStyleBackColor = false;
            this.buttonRunStatus.Click += new System.EventHandler(this.buttonRunStatus_Click);
            // 
            // labelCrawlOption
            // 
            this.labelCrawlOption.AutoSize = true;
            this.labelCrawlOption.BackColor = System.Drawing.Color.Transparent;
            this.labelCrawlOption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCrawlOption.ForeColor = System.Drawing.Color.White;
            this.labelCrawlOption.Location = new System.Drawing.Point(0, 5);
            this.labelCrawlOption.Name = "labelCrawlOption";
            this.labelCrawlOption.Size = new System.Drawing.Size(74, 21);
            this.labelCrawlOption.TabIndex = 2;
            this.labelCrawlOption.Text = "수집옵션";
            this.labelCrawlOption.Click += new System.EventHandler(this.labelCrawlOption_Click);
            // 
            // labelCrawlTerm
            // 
            this.labelCrawlTerm.AutoSize = true;
            this.labelCrawlTerm.BackColor = System.Drawing.Color.Transparent;
            this.labelCrawlTerm.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCrawlTerm.ForeColor = System.Drawing.Color.White;
            this.labelCrawlTerm.Location = new System.Drawing.Point(0, 30);
            this.labelCrawlTerm.Name = "labelCrawlTerm";
            this.labelCrawlTerm.Size = new System.Drawing.Size(99, 15);
            this.labelCrawlTerm.TabIndex = 7;
            this.labelCrawlTerm.Text = "수집간격 - 1시간";
            this.labelCrawlTerm.Click += new System.EventHandler(this.labelCrawlTerm_Click);
            // 
            // labelCurrentAddr
            // 
            this.labelCurrentAddr.AutoSize = true;
            this.labelCurrentAddr.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentAddr.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCurrentAddr.ForeColor = System.Drawing.Color.White;
            this.labelCurrentAddr.Location = new System.Drawing.Point(0, 5);
            this.labelCurrentAddr.Name = "labelCurrentAddr";
            this.labelCurrentAddr.Size = new System.Drawing.Size(74, 21);
            this.labelCurrentAddr.TabIndex = 2;
            this.labelCurrentAddr.Text = "현재주소";
            this.labelCurrentAddr.Click += new System.EventHandler(this.labelCurrentAddr_Click);
            // 
            // labelAddr
            // 
            this.labelAddr.AutoSize = true;
            this.labelAddr.BackColor = System.Drawing.Color.Transparent;
            this.labelAddr.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelAddr.ForeColor = System.Drawing.Color.White;
            this.labelAddr.Location = new System.Drawing.Point(0, 30);
            this.labelAddr.Name = "labelAddr";
            this.labelAddr.Size = new System.Drawing.Size(130, 15);
            this.labelAddr.TabIndex = 1;
            this.labelAddr.Text = "제주도 제주시 일도2동";
            this.labelAddr.Click += new System.EventHandler(this.labelAddr_Click);
            // 
            // buttonAddr
            // 
            this.buttonAddr.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonAddr.FlatAppearance.BorderSize = 0;
            this.buttonAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddr.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonAddr.Location = new System.Drawing.Point(0, 0);
            this.buttonAddr.Name = "buttonAddr";
            this.buttonAddr.Size = new System.Drawing.Size(230, 50);
            this.buttonAddr.TabIndex = 0;
            this.buttonAddr.TabStop = false;
            this.buttonAddr.UseVisualStyleBackColor = false;
            this.buttonAddr.Click += new System.EventHandler(this.buttonAddr_Click);
            // 
            // buttonCrawlOption
            // 
            this.buttonCrawlOption.BackColor = System.Drawing.Color.Orange;
            this.buttonCrawlOption.FlatAppearance.BorderSize = 0;
            this.buttonCrawlOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCrawlOption.Location = new System.Drawing.Point(230, 0);
            this.buttonCrawlOption.Name = "buttonCrawlOption";
            this.buttonCrawlOption.Size = new System.Drawing.Size(130, 50);
            this.buttonCrawlOption.TabIndex = 0;
            this.buttonCrawlOption.TabStop = false;
            this.buttonCrawlOption.UseVisualStyleBackColor = false;
            this.buttonCrawlOption.Click += new System.EventHandler(this.buttonCrawlOption_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(800, 372);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.Text = "Connecting MongoDB....\r\nConnected MongoDB.\r\nInsert Data - 2018-06-20";
            // 
            // contextMenuStripAddress
            // 
            this.contextMenuStripAddress.Name = "contextMenuStripAddress";
            this.contextMenuStripAddress.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStripCrawlOption
            // 
            this.contextMenuStripCrawlOption.Name = "contextMenuStripCrawlOption";
            this.contextMenuStripCrawlOption.Size = new System.Drawing.Size(181, 26);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "WeatherCrawler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 새로운주소ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 옵션ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 끝내기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 주소ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수집ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 모든주소수집실행ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 모든주소수집정지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도움말ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonAddr;
        private System.Windows.Forms.Label labelCurrentAddr;
        private System.Windows.Forms.Label labelAddr;
        private System.Windows.Forms.Button buttonCrawlOption;
        private System.Windows.Forms.Label labelCrawlOption;
        private System.Windows.Forms.Label labelCrawlTerm;
        private System.Windows.Forms.Button buttonRunStatus;
        private System.Windows.Forms.Label labelRunLastTime;
        private System.Windows.Forms.Label labelRunStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAddress;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCrawlOption;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

