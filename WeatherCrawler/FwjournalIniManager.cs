using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public class AddressLine
    {
        public string IndexNo { get; set; }
        public string Address { get; set; }
        public string Nx { get; set; }
        public string Ny { get; set; }
        public string CrawlTerm { get; set; }
        public string CrawlStatus { get; set; }
    }

    class FwjournalIniManager
    {
        public string FwjIni { get; set; }

        public string IpAddress { get; set; }
        public string DbName { get; set; }
        public string CollectionName { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public List<AddressLine> Addresses = new List<AddressLine>();

        public FwjournalIniManager()
        {
            FwjIni = "fwjournal.ini";
        }

        public void ReadIni()
        {
            // StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            StreamReader sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
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
                MessageBox.Show("영농일지 주소 정보가 없습니다3");
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

        public void ReadAddress()
        {
            // StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            StreamReader sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다4");
            }
            else
            {
                // sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다5");
                }
                else
                {
                    while (null != readLine && "" != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        AddressLine tmpAL = new AddressLine();
                        tmpAL.IndexNo = addressInfo[0];
                        tmpAL.Address = addressInfo[1];
                        tmpAL.Nx = addressInfo[2];
                        tmpAL.Ny = addressInfo[3];
                        tmpAL.CrawlTerm = addressInfo[4];
                        tmpAL.CrawlStatus = addressInfo[5];
                        Addresses.Add(tmpAL);
                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }
        }

        public void DeleteAddressLine()
        {
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다6");
            }
            else
            {
                sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
                string readLine = sr.ReadLine();    // DB 접속정보 읽기
                sr.Close();

                if (File.Exists(FwjIni))
                {
                    File.Delete(FwjIni);
                }

                StreamWriter sw = new StreamWriter(FwjIni, true, System.Text.Encoding.Default);
                sw.WriteLine(readLine);
                sw.Close();
            }
        }

        public void WriteCrawlTerm(string indexNo, string crawlTerm)
        {
            List<string> readLines = new List<string>();

            // readLines에 기존데이터 읽기
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다7");
            }
            else
            {
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기
                readLines.Add(readLine);

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다8");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        string addressIndex = addressInfo[0];

                        if (addressIndex == indexNo)
                        {
                            readLine = addressIndex + "|" +
                                       addressInfo[1] + "|" +
                                       addressInfo[2] + "|" +
                                       addressInfo[3] + "|" +
                                       crawlTerm + "|" +
                                       addressInfo[5];
                            readLines.Add(readLine);
                        }
                        else
                        {
                            readLines.Add(readLine);
                        }
                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(FwjIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void WriteCrawlStatus(string indexNo, string crawlStatus)
        {
            List<string> readLines = new List<string>();

            // readLines에 기존데이터 읽기
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다9");
            }
            else
            {
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기
                readLines.Add(readLine);

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다10");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        string addressIndex = addressInfo[0];

                        if (addressIndex == indexNo)
                        {
                            readLine = addressIndex + "|" +
                                       addressInfo[1] + "|" +
                                       addressInfo[2] + "|" +
                                       addressInfo[3] + "|" +
                                       addressInfo[4] + "|" +
                                       crawlStatus;
                            readLines.Add(readLine);
                        }
                        else
                        {
                            readLines.Add(readLine);
                        }
                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(FwjIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void WriteAllCrawlStatus(string crawlStatus)
        {
            List<string> readLines = new List<string>();

            // readLines에 기존데이터 읽기
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다11");
            }
            else
            {
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기
                readLines.Add(readLine);

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다12");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        string addressIndex = addressInfo[0];
                        readLine = addressIndex + "|" +
                                    addressInfo[1] + "|" +
                                    addressInfo[2] + "|" +
                                    addressInfo[3] + "|" +
                                    addressInfo[4] + "|" +
                                    crawlStatus;
                        readLines.Add(readLine);
                                                
                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(FwjIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public bool IsExistSameAddress(string address)
        {
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("fwjournal.ini의 주소 정보가 없습니다");
            }
            else
            {
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기            

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("fwjournal.ini의 주소 정보가 없습니다");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        if (addressInfo[1] == address)
                        {
                            return true;
                        }

                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }

            return false;
        }

        public void DeleteAddressByIndexNo(string indexNo)
        {
            List<string> readLines = new List<string>();

            // readLines에 기존데이터 읽기
            StreamReader sr = new StreamReader(new FileStream(FwjIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다13");
            }
            else
            {
                sr = new StreamReader(FwjIni, System.Text.Encoding.Default, true);
                string readLine = sr.ReadLine();    // DB 접속정보 읽기
                readLines.Add(readLine);

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다14");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
                    {
                        string[] addressInfo = readLine.Split('|');
                        string addressIndex = addressInfo[0];

                        if (addressIndex == indexNo)
                        {
                        }
                        else
                        {
                            readLines.Add(readLine);
                        }
                        readLine = sr.ReadLine();
                    }
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(FwjIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
