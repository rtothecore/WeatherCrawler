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

        public void WriteAddress(string address, double nx, double ny)
        {
            // 인덱스번호 얻기
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();
            string maxAddressIndex = null;

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다");
            }
            else
            {
                sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
                string readLine = sr.ReadLine();    // DB 접속정보 읽기

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    maxAddressIndex = "f0";
                }
                else
                {
                    List<string> addressindexes = new List<string>();

                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        string addressIndex = addressInfo[0];
                        addressindexes.Add(addressIndex);
                        readLine = sr.ReadLine();
                    }
                    maxAddressIndex = UtilManager.GetMaxAddressIndex(addressindexes);
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(FwjIni, true, System.Text.Encoding.Default);
            sw.WriteLine(maxAddressIndex + "|" +
                         address + "|" +
                         nx + "|" +
                         ny + "|" +
                         "1H" + "|" +
                         "S");
            sw.Close();
        }
    }
}
