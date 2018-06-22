using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    class FwjournalIniManager
    {
        public string FwjIni { get; set; }
        public string IpAddress { get; set; }
        public string DbName { get; set; }
        public string CollectionName { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public FwjournalIniManager()
        {
            FwjIni = "fwjournal.ini";
        }

        public void ReadIni()
        {
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 DB 접속 정보가 없습니다");
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
