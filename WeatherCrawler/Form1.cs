using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public partial class Form1 : Form
    {
        DbIniManager diManager = null;

        CrawlManager cManager = null;

        string currentSelectedIndex = null;
        string currentSelectedRunStatus = null;

        public Form1()
        {
            InitializeComponent();

            // 현재주소 버튼
            labelCurrentAddr.Parent = buttonAddr;
            labelCurrentAddr.BackColor = Color.Transparent;
            Fonts.Reload(16);
            labelCurrentAddr.Font = Fonts.FontAwesome;
            labelCurrentAddr.Text = "현재주소                       " + Fonts.fa.sort_down;
            labelCurrentAddr.MouseEnter += OnMouseEnterButtonAddr;
            labelCurrentAddr.MouseLeave += OnMouseLeaveButtonAddr;

            labelAddr.Parent = buttonAddr;
            labelAddr.BackColor = Color.Transparent;
            labelAddr.MouseEnter += OnMouseEnterButtonAddr;
            labelAddr.MouseLeave += OnMouseLeaveButtonAddr;

            buttonAddr.BackColor = Color.Red;
            buttonAddr.MouseEnter += OnMouseEnterButtonAddr;
            buttonAddr.MouseLeave += OnMouseLeaveButtonAddr;

            // 수집옵션 버튼
            labelCrawlOption.Parent = buttonCrawlOption;
            labelCrawlOption.BackColor = Color.Transparent;
            labelCrawlOption.Font = Fonts.FontAwesome;
            labelCrawlOption.Text = Fonts.fa.cogs + "  수집옵션";
            labelCrawlOption.MouseEnter += OnMouseEnterButtonCrawlOption;
            labelCrawlOption.MouseLeave += OnMouseLeaveButtonCrawlOption;

            labelCrawlTerm.Parent = buttonCrawlOption;
            labelCrawlTerm.BackColor = Color.Transparent;
            labelCrawlTerm.MouseEnter += OnMouseEnterButtonCrawlOption;
            labelCrawlTerm.MouseLeave += OnMouseLeaveButtonCrawlOption;

            buttonCrawlOption.BackColor = Color.Orange;
            buttonCrawlOption.MouseEnter += OnMouseEnterButtonCrawlOption;
            buttonCrawlOption.MouseLeave += OnMouseLeaveButtonCrawlOption;

            // 수집상태 버튼
            labelRunStatus.Parent = buttonRunStatus;
            labelRunStatus.BackColor = Color.Transparent;
            labelRunStatus.Font = Fonts.FontAwesome;
            labelRunStatus.Text = Fonts.fa.recycle + "  " + "수집중";
            labelRunStatus.MouseEnter += OnMouseEnterButtonRunStatus;
            labelRunStatus.MouseLeave += OnMouseLeaveButtonRunStatus;

            labelRunLastTime.Parent = buttonRunStatus;
            labelRunLastTime.BackColor = Color.Transparent;
            labelRunLastTime.MouseEnter += OnMouseEnterButtonRunStatus;
            labelRunLastTime.MouseLeave += OnMouseLeaveButtonRunStatus;

            buttonRunStatus.BackColor = Color.DeepSkyBlue;
            buttonRunStatus.MouseEnter += OnMouseEnterButtonRunStatus;
            buttonRunStatus.MouseLeave += OnMouseLeaveButtonRunStatus;

            // 현재주소 메뉴
            InitializeContextMenuStripAddress();

            // 수집옵션 메뉴
            InitializeContextMenuStripCrawlOption();

            InitializeDIManager();
            RunCrawlAndCheck();
        }

        // 수집간격 옵션 클릭시 이벤트 함수
        void optionItem_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            FwjournalIniManager fjiManager = new FwjournalIniManager();
            labelCrawlTerm.Text = "수집간격 - " + UtilManager.ConvertCrawlTerm(clickedItem.Tag.ToString());
            fjiManager.WriteCrawlTerm(currentSelectedIndex, clickedItem.Tag.ToString());
            InitializeContextMenuStripAddress();
        }

        // 수집간격 옵션 버튼 메뉴 초기화
        private void InitializeContextMenuStripCrawlOption()
        {
            ToolStripItem optionItem1 = contextMenuStripCrawlOption.Items.Add("1시간");
            optionItem1.Tag = "1H";
            optionItem1.Click += new EventHandler(optionItem_Click);

            ToolStripItem optionItem2 = contextMenuStripCrawlOption.Items.Add("30분");
            optionItem2.Tag = "30M";
            optionItem2.Click += new EventHandler(optionItem_Click);

            ToolStripItem optionItem3 = contextMenuStripCrawlOption.Items.Add("1분");
            optionItem3.Tag = "1M";
            optionItem3.Click += new EventHandler(optionItem_Click);
        }

        // 주소 클릭시 이벤트 함수
        void addressItem_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            AddressLine tmpAL = (AddressLine)clickedItem.Tag;            
            currentSelectedIndex = tmpAL.IndexNo;

            // 수집 주소
            labelAddr.Text = tmpAL.Address;

            // 수집 간격 옵션
            labelCrawlTerm.Text = "수집간격 - " + UtilManager.ConvertCrawlTerm(tmpAL.CrawlTerm);

            // 수집 상태
            if ("R" == tmpAL.CrawlStatus)
                labelRunStatus.Text = Fonts.fa.recycle + "  " + UtilManager.ConvertCrawlStatus(tmpAL.CrawlStatus);
            else
                labelRunStatus.Text = Fonts.fa.hand_stop_o + "  " + UtilManager.ConvertCrawlStatus(tmpAL.CrawlStatus);

            currentSelectedRunStatus = tmpAL.CrawlStatus;

        }

        // 현재 주소 버튼 메뉴 초기화
        private void InitializeContextMenuStripAddress()
        {
            // 이미 메뉴에 아이템이 생성되어 있다면 모두 지움
            if (0 < contextMenuStripAddress.Items.Count)
                contextMenuStripAddress.Items.Clear();

            // fwjournal.ini 읽어서 주소 메뉴에 추가
            FwjournalIniManager fiManager = new FwjournalIniManager();
            fiManager.ReadAddress();
            foreach(var address in fiManager.Addresses)
            {
                ToolStripItem addressItem = contextMenuStripAddress.Items.Add(address.Address + "               " + 
                                                                       UtilManager.ConvertCrawlTerm(address.CrawlTerm) + "               " + 
                                                                       UtilManager.ConvertCrawlStatus(address.CrawlStatus));
                addressItem.Tag = address;
                addressItem.Click += new EventHandler(addressItem_Click);
            }

            // address.ini 읽어서 메뉴에 추가
        }

        private void InitializeDIManager()
        {
            diManager = new DbIniManager();
            diManager.ReadIni();
        }

        private void RunCrawlAndCheck()
        {
            // fwjournal.ini, address.ini 읽어서 수집시작
            cManager = new CrawlManager();

            // fwjournal DB Checker 시작
            FwjournalDBChecker fjDbChecker = new FwjournalDBChecker(cManager.schedulerForFJ);
        }

        private void labelCurrentAddr_Click(object sender, EventArgs e)
        {
            buttonAddr_Click(sender, e);
        }

        private void labelAddr_Click(object sender, EventArgs e)
        {
            buttonAddr_Click(sender, e);
        }

        private void buttonAddr_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonAddr_Click");
            contextMenuStripAddress.Show(buttonAddr, 0, 50);
        }

        private void OnMouseEnterButtonAddr(object sender, EventArgs e)
        {
            buttonAddr.BackColor = Color.Teal; 
        }

        private void OnMouseLeaveButtonAddr(object sender, EventArgs e)
        {
            buttonAddr.BackColor = Color.Red;
        }

        private void buttonCrawlOption_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonCrawlOption_Click");
            contextMenuStripCrawlOption.Show(buttonAddr, 230, 50);
        }

        private void OnMouseEnterButtonCrawlOption(object sender, EventArgs e)
        {
            buttonCrawlOption.BackColor = Color.Olive;
        }

        private void OnMouseLeaveButtonCrawlOption(object sender, EventArgs e)
        {
            buttonCrawlOption.BackColor = Color.Orange;
        }

        private void labelCrawlOption_Click(object sender, EventArgs e)
        {
            buttonCrawlOption_Click(sender, e);
        }

        private void labelCrawlTerm_Click(object sender, EventArgs e)
        {
            buttonCrawlOption_Click(sender, e);
        }

        private async void PauseJob()
        {
            if (currentSelectedIndex.Contains("f"))
            {
                JobKey jobKey = JobKey.Create(currentSelectedIndex, "MyOwnGroup");   
                await cManager.schedulerForFJ.PauseJob(jobKey);
            }
            else
                await cManager.schedulerForAddr.PauseJob(new JobKey(currentSelectedIndex));
        }

        private async void ResumeJob()
        {
            if (currentSelectedIndex.Contains("f"))
                await cManager.schedulerForFJ.ResumeJob(new JobKey(currentSelectedIndex));
            else
                await cManager.schedulerForAddr.ResumeJob(new JobKey(currentSelectedIndex));
        }

        // 수집 상태 토글 버튼
        private void buttonRunStatus_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonRunStatus_Click");
            FwjournalIniManager fjiManager = new FwjournalIniManager();

            if ("R" == currentSelectedRunStatus)
            {
                PauseJob();
                labelRunStatus.Text = Fonts.fa.hand_stop_o + "  " + UtilManager.ConvertCrawlStatus("S");
                currentSelectedRunStatus = "S";
                fjiManager.WriteCrawlStatus(currentSelectedIndex, "S");
            }
            else
            {
                ResumeJob();
                labelRunStatus.Text = Fonts.fa.recycle + "  " + UtilManager.ConvertCrawlStatus("R");
                currentSelectedRunStatus = "R";
                fjiManager.WriteCrawlStatus(currentSelectedIndex, "R");
            }

            InitializeContextMenuStripAddress();
        }

        private void labelRunStatus_Click(object sender, EventArgs e)
        {
            buttonRunStatus_Click(sender, e);
        }

        private void labelRunLastTime_Click(object sender, EventArgs e)
        {
            buttonRunStatus_Click(sender, e);
        }

        private void OnMouseEnterButtonRunStatus(object sender, EventArgs e)
        {
            buttonRunStatus.BackColor = Color.Blue;
        }

        private void OnMouseLeaveButtonRunStatus(object sender, EventArgs e)
        {
            buttonRunStatus.BackColor = Color.DeepSkyBlue;
        }

        private void 새로운주소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewAddress dlgForNewAddress = new FormNewAddress();
            dlgForNewAddress.ShowDialog();
        }

        private void 옵션ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOption dlgForOption = new FormOption();
            dlgForOption.SetDBINI(diManager);
            diManager.SetFormOption(dlgForOption);
            dlgForOption.ShowDialog();
        }
    }
}
