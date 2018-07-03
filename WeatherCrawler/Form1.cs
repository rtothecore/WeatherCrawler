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
        FwjournalDBChecker fjDbChecker = null;

        LogOutputManager loManagerForCommon = null;
        LogOutputManager loManagerForAddress = null;

        string currentSelectedIndex = null;
        string currentSelectedRunStatus = null;

        bool isAddedNewAddress = false;

        public Form1()
        {
            L4Logger l4Logger = new L4Logger("common.log");
            l4Logger.Add("App Started!");
            l4Logger.Close();

            InitializeComponent();

            // 프로그램 실행시 fwjournal.ini의 첫 주소를 선택한 걸로 셋팅
            InitializeToolBar();

            // 현재주소 메뉴
            InitializeContextMenuStripAddress();

            // 수집옵션 메뉴
            InitializeContextMenuStripCrawlOption();

            InitializeDIManager();
            RunCrawlAndCheck();

            // 로그파일 출력
            loManagerForCommon = new LogOutputManager("common", textBoxCommonLog);
            loManagerForAddress = new LogOutputManager("f0", textBoxPrivateLog);
        }

        // 툴바 초기화
        void InitializeToolBar()
        {
            // fwjournal.ini 읽기
            FwjournalIniManager fiManager = new FwjournalIniManager();
            fiManager.ReadAddress();            

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

            buttonAddr.BackColor = Color.FromArgb(45, 45, 48);
            buttonAddr.MouseEnter += OnMouseEnterButtonAddr;
            buttonAddr.MouseLeave += OnMouseLeaveButtonAddr;

            labelAddr.Text = fiManager.Addresses[0].Address;

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

            buttonCrawlOption.BackColor = Color.FromArgb(45, 45, 48);
            buttonCrawlOption.MouseEnter += OnMouseEnterButtonCrawlOption;
            buttonCrawlOption.MouseLeave += OnMouseLeaveButtonCrawlOption;

            labelCrawlTerm.Text = "수집간격 - " + UtilManager.ConvertCrawlTerm(fiManager.Addresses[0].CrawlTerm);

            // 수집상태 버튼
            labelRunStatus.Parent = buttonRunStatus;
            labelRunStatus.BackColor = Color.Transparent;
            labelRunStatus.Font = Fonts.FontAwesome;
            // labelRunStatus.Text = Fonts.fa.recycle + "  " + "수집중";
            labelRunStatus.MouseEnter += OnMouseEnterButtonRunStatus;
            labelRunStatus.MouseLeave += OnMouseLeaveButtonRunStatus;

            labelRunLastTime.Parent = buttonRunStatus;
            labelRunLastTime.BackColor = Color.Transparent;
            labelRunLastTime.MouseEnter += OnMouseEnterButtonRunStatus;
            labelRunLastTime.MouseLeave += OnMouseLeaveButtonRunStatus;

            buttonRunStatus.BackColor = Color.FromArgb(45, 45, 48);
            buttonRunStatus.MouseEnter += OnMouseEnterButtonRunStatus;
            buttonRunStatus.MouseLeave += OnMouseLeaveButtonRunStatus;

            labelRunStatus.Text = Fonts.fa.recycle + "  " + UtilManager.ConvertCrawlStatus(fiManager.Addresses[0].CrawlStatus);

            // 변수 초기화
            currentSelectedIndex = "f0";
            currentSelectedRunStatus = fiManager.Addresses[0].CrawlStatus;            
        }

        // 툴바 리셋
        void ResetToolBar()
        {
            // fwjournal.ini 읽기
            FwjournalIniManager fiManager = new FwjournalIniManager();
            fiManager.ReadAddress();

            // 현재주소 버튼
            labelAddr.Text = fiManager.Addresses[0].Address;

            // 수집옵션 버튼                
            labelCrawlTerm.Text = "수집간격 - " + UtilManager.ConvertCrawlTerm(fiManager.Addresses[0].CrawlTerm);

            // 수집상태 버튼
            labelRunStatus.Text = Fonts.fa.recycle + "  " + UtilManager.ConvertCrawlStatus(fiManager.Addresses[0].CrawlStatus);

            // 변수 초기화
            currentSelectedIndex = "f0";
            currentSelectedRunStatus = fiManager.Addresses[0].CrawlStatus;
        }

        // 수집간격 옵션 클릭시 이벤트 함수
        void optionItem_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            labelCrawlTerm.Text = "수집간격 - " + UtilManager.ConvertCrawlTerm(clickedItem.Tag.ToString());

            if (currentSelectedIndex.Contains("f"))
            {
                FwjournalIniManager fjiManager = new FwjournalIniManager();
                fjiManager.WriteCrawlTerm(currentSelectedIndex, clickedItem.Tag.ToString());
            }
            else
            {
                AddressIniManager aiManager = new AddressIniManager();
                aiManager.WriteCrawlTerm(currentSelectedIndex, clickedItem.Tag.ToString());
            }

            // 현재 주소 리스트 초기화
            InitializeContextMenuStripAddress();

            RunCrawlAndCheck();
        }

        // 수집간격 옵션 버튼 메뉴 초기화
        private void InitializeContextMenuStripCrawlOption()
        {
            ToolStripItem optionItem1 = contextMenuStripCrawlOption.Items.Add("1시간");
            optionItem1.Tag = "1H";
            optionItem1.Click += new EventHandler(optionItem_Click);
            optionItem1.ForeColor = Color.White;

            ToolStripItem optionItem2 = contextMenuStripCrawlOption.Items.Add("30분");
            optionItem2.Tag = "30M";
            optionItem2.Click += new EventHandler(optionItem_Click);
            optionItem2.ForeColor = Color.White;

            ToolStripItem optionItem3 = contextMenuStripCrawlOption.Items.Add("1분");
            optionItem3.Tag = "1M";
            optionItem3.Click += new EventHandler(optionItem_Click);
            optionItem3.ForeColor = Color.White;
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

            // 로그
            loManagerForAddress.StopOutput();
            loManagerForAddress = new LogOutputManager(tmpAL.IndexNo, textBoxPrivateLog);
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
                /*
                ToolStripItem addressItem = contextMenuStripAddress.Items.Add(address.Address + "               " + 
                                                                       UtilManager.ConvertCrawlTerm(address.CrawlTerm) + "               " + 
                                                                       UtilManager.ConvertCrawlStatus(address.CrawlStatus));
                */
                ToolStripItem addressItem = contextMenuStripAddress.Items.Add(address.Address.PadRight(25, '-') +
                                                                       UtilManager.ConvertCrawlTerm(address.CrawlTerm).PadRight(25, '-') +
                                                                       UtilManager.ConvertCrawlStatus(address.CrawlStatus));
                addressItem.Tag = address;
                addressItem.Click += new EventHandler(addressItem_Click);
                addressItem.ForeColor = Color.White;
            }

            // address.ini 읽어서 메뉴에 추가
            AddressIniManager aiManager = new AddressIniManager();
            aiManager.ReadAddress();
            foreach(var address in aiManager.Addresses)
            {
                /*
                ToolStripItem addressItem = contextMenuStripAddress.Items.Add(address.Address + "               " +
                                                                       UtilManager.ConvertCrawlTerm(address.CrawlTerm) + "               " +
                                                                       UtilManager.ConvertCrawlStatus(address.CrawlStatus));
                */
                ToolStripItem addressItem = contextMenuStripAddress.Items.Add(address.Address.PadRight(25, '-') +
                                                                       UtilManager.ConvertCrawlTerm(address.CrawlTerm).PadRight(25, '-') +
                                                                       UtilManager.ConvertCrawlStatus(address.CrawlStatus));
                addressItem.Tag = address;
                addressItem.Click += new EventHandler(addressItem_Click);
                addressItem.ForeColor = Color.White;
            }
        }

        private void InitializeDIManager()
        {
            diManager = new DbIniManager();
            diManager.ReadIni();
        }

        private void RunCrawlAndCheck()
        {
            L4Logger l4Logger = new L4Logger("common.log");

            // 수집중이라면 스케쥴러를 모두 셧다운 시킴
            if(null != cManager)
            {
                if (null != cManager.schedulerForFJ && cManager.schedulerForFJ.IsStarted)
                {
                    l4Logger.Add("Shutdown CrawlManager FJ Tasks");                    
                    cManager.schedulerForFJ.PauseAll();
                    cManager.schedulerForFJ.Shutdown();
                }

                if(null != cManager.schedulerForAddr && cManager.schedulerForAddr.IsStarted)
                {
                    l4Logger.Add("Shutdown CrawlManager Addr Tasks");
                    cManager.schedulerForAddr.PauseAll();
                    cManager.schedulerForAddr.Shutdown();
                }
            }

            // fwjournal.ini, address.ini 읽어서 수집시작
            l4Logger.Add("Start CrawlManager Tasks");
            cManager = new CrawlManager();

            // DB 체크 스케쥴러가 실행중이라면 셧다운 시킴
            if (null != fjDbChecker &&
                fjDbChecker.CrawlScheduler.IsStarted)
            {
                l4Logger.Add("Shutdown FwjournalDBChecker Tasks");

                fjDbChecker.CrawlScheduler.PauseAll();
                fjDbChecker.CrawlScheduler.Shutdown();
            }

            // fwjournal DB Checker 시작
            l4Logger.Add("Start FwjournalDBChecker Tasks");
            l4Logger.Close();
            fjDbChecker = new FwjournalDBChecker(cManager.schedulerForFJ);
        }

        private void labelCurrentAddr_Click(object sender, EventArgs e)
        {
            buttonAddr_Click(sender, e);
        }

        private void labelAddr_Click(object sender, EventArgs e)
        {
            buttonAddr_Click(sender, e);
        }

        // 현재주소 버튼
        private void buttonAddr_Click(object sender, EventArgs e)
        {
            // Console.WriteLine("buttonAddr_Click");
            InitializeContextMenuStripAddress();
            contextMenuStripAddress.Show(buttonAddr, 0, 50);

            if (isAddedNewAddress)
            {
                RunCrawlAndCheck();
            }

            isAddedNewAddress = false;
        }

        private void OnMouseEnterButtonAddr(object sender, EventArgs e)
        {
            buttonAddr.BackColor = Color.FromArgb(63, 63, 70); 
        }

        private void OnMouseLeaveButtonAddr(object sender, EventArgs e)
        {
            buttonAddr.BackColor = Color.FromArgb(45, 45, 48); ;
        }

        private void buttonCrawlOption_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonCrawlOption_Click");
            contextMenuStripCrawlOption.Show(buttonAddr, 230, 50);
        }

        private void OnMouseEnterButtonCrawlOption(object sender, EventArgs e)
        {
            buttonCrawlOption.BackColor = Color.FromArgb(63, 63, 70);
        }

        private void OnMouseLeaveButtonCrawlOption(object sender, EventArgs e)
        {
            buttonCrawlOption.BackColor = Color.FromArgb(45, 45, 48);
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
                try
                {
                    JobKey jobKey = JobKey.Create(currentSelectedIndex);
                    if (await cManager.schedulerForFJ.CheckExists(jobKey))
                    {
                        Console.WriteLine("cManager.schedulerForFJ.CheckExists:{0}", jobKey);
                        await cManager.schedulerForFJ.PauseJob(jobKey);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                try
                {
                    JobKey jobKey = JobKey.Create(currentSelectedIndex);
                    if (await cManager.schedulerForAddr.CheckExists(jobKey))
                    {
                        Console.WriteLine("cManager.schedulerForAddr.CheckExists:{0}", jobKey);
                        await cManager.schedulerForAddr.PauseJob(jobKey);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }    
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
            // Console.WriteLine("buttonRunStatus_Click");
            FwjournalIniManager fjiManager = new FwjournalIniManager();
            AddressIniManager aiManager = new AddressIniManager();

            if ("R" == currentSelectedRunStatus)
            {
                PauseJob();
                labelRunStatus.Text = Fonts.fa.hand_stop_o + "  " + UtilManager.ConvertCrawlStatus("S");
                currentSelectedRunStatus = "S";
                if (currentSelectedIndex.Contains("f"))
                    fjiManager.WriteCrawlStatus(currentSelectedIndex, "S");
                else
                    aiManager.WriteCrawlStatus(currentSelectedIndex, "S");
            }
            else
            {
                ResumeJob();
                labelRunStatus.Text = Fonts.fa.recycle + "  " + UtilManager.ConvertCrawlStatus("R");
                currentSelectedRunStatus = "R";
                if (currentSelectedIndex.Contains("f"))
                    fjiManager.WriteCrawlStatus(currentSelectedIndex, "R");
                else
                    aiManager.WriteCrawlStatus(currentSelectedIndex, "R");
            }

            // 현재 주소 리스트 초기화
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
            buttonRunStatus.BackColor = Color.FromArgb(63, 63, 70);
        }

        private void OnMouseLeaveButtonRunStatus(object sender, EventArgs e)
        {
            buttonRunStatus.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void 새로운주소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewAddress dlgForNewAddress = new FormNewAddress();
            if (DialogResult.OK == dlgForNewAddress.ShowDialog())
            {
                L4Logger l4Logger = new L4Logger("common.log");
                l4Logger.Add("New address added!");
                l4Logger.Close();

                isAddedNewAddress = true;
            }
        }

        private void 옵션ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOption dlgForOption = new FormOption();
            dlgForOption.SetDBINI(diManager);
            diManager.SetFormOption(dlgForOption);
            dlgForOption.ShowDialog();
        }

        private void 모든주소수집실행ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 모든 작업 재실행
            cManager.schedulerForFJ.ResumeAll();
            cManager.schedulerForAddr.ResumeAll();

            // *.ini 파일 업데이트
            FwjournalIniManager fjiManager = new FwjournalIniManager();
            AddressIniManager aiManager = new AddressIniManager();
            fjiManager.WriteAllCrawlStatus("R");
            aiManager.WriteAllCrawlStatus("R");

            // 현재 주소 리스트 초기화
            InitializeContextMenuStripAddress();

            // 로그
            L4Logger l4Logger = new L4Logger("common.log");
            l4Logger.Add("All tasks resumed!");
            l4Logger.Close();
        }

        private void 모든주소수집정지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 모든 작업 정지
            cManager.schedulerForFJ.PauseAll();
            cManager.schedulerForAddr.PauseAll();

            // *.ini 파일 업데이트
            FwjournalIniManager fjiManager = new FwjournalIniManager();
            AddressIniManager aiManager = new AddressIniManager();
            fjiManager.WriteAllCrawlStatus("S");
            aiManager.WriteAllCrawlStatus("S");

            // 현재 주소 리스트 초기화
            InitializeContextMenuStripAddress();

            // 로그
            L4Logger l4Logger = new L4Logger("common.log");
            l4Logger.Add("All tasks paused!");
            l4Logger.Close();
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentSelectedIndex.Contains("f"))
            {
                // 기본적으로 fwjournal.ini 는 삭제못하도록 함.
                /*
                FwjournalIniManager fiManager = new FwjournalIniManager();
                fiManager.DeleteAddressByIndexNo(currentSelectedIndex);
                */
                MessageBox.Show("영농일지 관련 데이터(fwjournal.ini)는 삭제할 수 없습니다");
            }
            else
            {
                // 해당 작업 멈추고 삭제
                JobKey jobKey = JobKey.Create(currentSelectedIndex);
                cManager.schedulerForAddr.PauseJob(jobKey);
                cManager.schedulerForAddr.DeleteJob(jobKey);

                // address.ini 에서 삭제
                AddressIniManager aiManager = new AddressIniManager();
                aiManager.DeleteAddressByIndexNo(currentSelectedIndex);

                // 공통 로그파일에 삭제로그 남기기
                L4Logger l4Logger = new L4Logger("common.log");
                l4Logger.Add(currentSelectedIndex + " is deleted");
                l4Logger.Close();

                // 삭제할 인덱스 저장
                // string indexForDelete = currentSelectedIndex;

                // 현재 주소 리스트 초기화
                InitializeContextMenuStripAddress();

                // 툴바 초기화
                ResetToolBar();

                // 로그 출력창 설정
                loManagerForAddress.StopOutput();
                loManagerForAddress = new LogOutputManager(currentSelectedIndex, textBoxPrivateLog);

                // 개별 로그파일 삭제
                // UtilManager.DeleteLogFile(indexForDelete + ".log");
            }
        }
    }
}
