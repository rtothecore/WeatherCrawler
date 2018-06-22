using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public class DbIniManager
    {
        public string DbIni { get; set; }
        public string IpAddress { get; set; }
        public string DbName { get; set; }
        public string CollectionName { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public DbIniManager()
        {
            DbIni = "db.ini";
        }

        public void Initialize()
        {
            StreamReader sr = new StreamReader(new FileStream(DbIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                FormOption dlgForOption = new FormOption();
                dlgForOption.SetDBINI(this);
                DialogResult dr = new DialogResult();
                dr = dlgForOption.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    // MessageBox.Show("User clicked OK button");
                    StreamWriter sw = new StreamWriter(new FileStream(DbIni, FileMode.OpenOrCreate));
                    IpAddress = dlgForOption.GetTextBoxIpAddress();
                    DbName = dlgForOption.GetTextBoxDBName();
                    CollectionName = dlgForOption.GetTextBoxCollectionName();
                    Id = dlgForOption.GetTextBoxId();
                    Pw = dlgForOption.GetTextBoxPw();

                    sw.WriteLine(IpAddress + "|" +
                                 DbName + "|" +
                                 CollectionName + "|" +
                                 Id + "|" +
                                 Pw);

                    sw.Close();
                }
                else if (dr == DialogResult.Cancel)
                {
                    // MessageBox.Show("User clicked Cancel button");
                }
            }
            else
            {
            }
        }

        public void SetFormOption(FormOption dlg)
        {
            StreamReader sr = new StreamReader(new FileStream(DbIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;

            if (0 == fileSize)
            {
                MessageBox.Show("DB 접속 정보가 없습니다");
            }
            else
            {
                string readLine = sr.ReadLine();
                string[] dbInfo = readLine.Split('|');
                dlg.SetTextBoxIpAddress(dbInfo[0]);
                dlg.SetTextBoxDBName(dbInfo[1]);
                dlg.SetTextBoxCollectionName(dbInfo[2]);
                dlg.SetTextBoxId(dbInfo[3]);
                dlg.SetTextBoxPw(dbInfo[4]);
            }
            sr.Close();
        }

        public void WriteIni(FormOption dlg)
        {
            StreamWriter sw = new StreamWriter(new FileStream(DbIni, FileMode.OpenOrCreate));
            sw.WriteLine(dlg.GetTextBoxIpAddress() + "|" +
                         dlg.GetTextBoxDBName() + "|" +
                         dlg.GetTextBoxCollectionName() + "|" +
                         dlg.GetTextBoxId() + "|" +
                         dlg.GetTextBoxPw());
            sw.Close();
        }

        public void DeleteIni()
        {
            if (File.Exists(DbIni))
            {
                System.IO.File.Delete(DbIni);
            }
        }

        public void ReadIni()
        {
            StreamReader sr = new StreamReader(new FileStream(DbIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;

            if (0 == fileSize)
            {
                MessageBox.Show("DB 접속 정보가 없습니다");
            }
            else
            {
                string readLine = sr.ReadLine();
                string[] dbInfo = readLine.Split('|');

                IpAddress = dbInfo[0];
                DbName = dbInfo[1];
                CollectionName = dbInfo[2];
                Id = dbInfo[3];
                Pw = dbInfo[4];
            }
            sr.Close();
        }
    }
}
