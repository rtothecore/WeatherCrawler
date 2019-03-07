using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{    
    class AddressIniManager
    {
        public string AddressIni { get; set; }

        public string IpAddress { get; set; }
        public string DbName { get; set; }
        public string CollectionName { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public List<AddressLine> Addresses = new List<AddressLine>();

        public AddressIniManager()
        {
            AddressIni = "address.ini";
        }

        public void WriteAddress(string address, double nx, double ny)
        {
            // 인덱스번호 얻기
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();
            string maxAddressIndex = "0";

            if (0 == fileSize)
            {
                MessageBox.Show("address.ini의 주소 정보가 없습니다-1");
            }
            else
            {
                sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    maxAddressIndex = "0";
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
                    maxAddressIndex = UtilManager.GetMaxAddressIndex2(addressindexes);
                }
                sr.Close();
            }

            // 쓰기            
            StreamWriter sw = new StreamWriter(AddressIni, true, System.Text.Encoding.Default);
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
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                // MessageBox.Show("address.ini의 주소 정보가 없습니다-2");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("address.ini의 주소 정보가 없습니다-3");
                }
                else
                {
                    while (null != readLine)     // 주소정보 읽기 시작
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

        public bool IsExistSameAddress(string address)
        {
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("address.ini의 주소 정보가 없습니다-4");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("address.ini의 주소 정보가 없습니다-5");
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
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("영농일지 주소 정보가 없습니다1");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("영농일지 주소 정보가 없습니다2");
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
            StreamWriter sw = new StreamWriter(AddressIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void WriteCrawlTerm(string indexNo, string crawlTerm)
        {
            List<string> readLines = new List<string>();

            // readLines에 기존데이터 읽기
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("address.ini 주소 정보가 없습니다");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("address.ini 주소 정보가 없습니다");
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
            StreamWriter sw = new StreamWriter(AddressIni, false, System.Text.Encoding.Default);
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
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("address.ini 주소 정보가 없습니다");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("address.ini 주소 정보가 없습니다");
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
            StreamWriter sw = new StreamWriter(AddressIni, false, System.Text.Encoding.Default);
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
            StreamReader sr = new StreamReader(new FileStream(AddressIni, FileMode.OpenOrCreate));
            long fileSize = sr.BaseStream.Length;
            sr.Close();

            if (0 == fileSize)
            {
                MessageBox.Show("address.ini 주소 정보가 없습니다");
            }
            else
            {
                sr = new StreamReader(AddressIni, System.Text.Encoding.Default, true);
                string readLine = null;

                if (null == (readLine = sr.ReadLine()))     // 주소정보 읽기 시작
                {
                    MessageBox.Show("address.ini 주소 정보가 없습니다");
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
            StreamWriter sw = new StreamWriter(AddressIni, false, System.Text.Encoding.Default);
            foreach (var line in readLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
