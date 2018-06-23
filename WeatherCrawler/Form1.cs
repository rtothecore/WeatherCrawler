using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public partial class Form1 : Form
    {
        DbIniManager diManager = null;

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

            diManager = new DbIniManager();
            diManager.Initialize();
            diManager.ReadIni();
            DbManager dm = new DbManager(diManager.IpAddress, diManager.DbName, diManager.CollectionName, diManager.Id, diManager.Pw);
            if(dm.Connect())
            {
            }
            else
            {
                MessageBox.Show("수집서버 DB에 접속할 수 없습니다");
            }
            FwjournalIniManager fwjIniManager = new FwjournalIniManager();
            fwjIniManager.ReadIni();
            DbManager dm2 = new DbManager(fwjIniManager.IpAddress, fwjIniManager.DbName, fwjIniManager.CollectionName, fwjIniManager.Id, fwjIniManager.Pw);
            if (dm2.Connect())
            {
            }
            else
            {
                MessageBox.Show("영농일지 DB에 접속할 수 없습니다");
            }
            List<string> addresss = dm2.ReadFwjournalLands();
            foreach(var address in addresss)
            {
                HttpClientManager hcManager = new HttpClientManager("http://maps.googleapis.com/maps/api/geocode/json", 
                                                                    "?sensor=false&language=ko&address=" + address,
                                                                    address);
            }
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

        private void buttonRunStatus_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonRunStatus_Click");
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
