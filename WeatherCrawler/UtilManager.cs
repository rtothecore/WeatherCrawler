using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    static class UtilManager
    {
        static UtilManager()
        {

        }

        static public string TrimAddress(string address)
        {
            string[] splitedAddress = address.Split(' ');
            return splitedAddress[0] + " " + splitedAddress[1] + " " + splitedAddress[2];
        }

        static public string GetMaxAddressIndex(List<string> addressindexes)
        {
            List<int> addressIndexVals = new List<int>();
            for(int i = 0; i < addressindexes.Count; i++)
            {
                string tmpIndex = addressindexes[i].Replace("f", "");
                addressIndexVals.Add(Int32.Parse(tmpIndex));
            }
            int maxAddressVal = addressIndexVals.Max();

            return "f" + (maxAddressVal + 1);
        }

        static public string GetMaxAddressIndex2(List<string> addressindexes)
        {
            List<int> addressIndexVals = new List<int>();
            for (int i = 0; i < addressindexes.Count; i++)
            {
                string tmpIndex = addressindexes[i].Replace("f", "");
                addressIndexVals.Add(Int32.Parse(tmpIndex));
            }
            int maxAddressVal = addressIndexVals.Max();

            return (maxAddressVal + 1).ToString();
        }

        static public string GetBaseDateTimeForForecastGrib()
        {
            DateTime now = DateTime.Now;
            DateTime resultTime = now;
            int currentMinutes = Int32.Parse(now.ToString("mm"));

            if(40 >= currentMinutes)
            {
                resultTime = now.AddHours(-1);
            }

            string result = resultTime.ToString("yyyyMMddHHmm").Remove(10, 2);
            result = result.Insert(10, "00");

            return result;
        }

        static public string GetUmdFromAddress(string address)
        {
            string[] splitedAddress = address.Split(' ');
            return splitedAddress[2];
        }

        static public string GetBaseDateTimeForForecastTime()
        {
            DateTime now = DateTime.Now;
            DateTime resultTime = now;
            int currentMinutes = Int32.Parse(now.ToString("mm"));

            if (45 >= currentMinutes)
            {
                resultTime = now.AddHours(-1);
            }

            string result = resultTime.ToString("yyyyMMddHHmm").Remove(10, 2);
            result = result.Insert(10, "00");

            return result;
        }

        static public string GetNowDateTime()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        static public string GetFTSRHour()
        {
            DateTime now = DateTime.Now;
            DateTime resultTime = now;
            int currentMinutes = Int32.Parse(now.ToString("mm"));

            if (45 >= currentMinutes)
            {
                resultTime = now.AddHours(-1);
            }

            string result = resultTime.ToString("HH");

            return result;
        }

        static public ForecastGribResult ReassembleDataForForecastGrib(ForecastGribResult data)
        {
            List<int> outIndex = new List<int>();

            if (null == data.response)
                return null;

            if (null == data.response.body)
                return null;

            if (null == data.response.body.items)
                return null;

            for (int i = 0; i < data.response.body.items.item.Count; i++)
            {
                if ("PTY" == data.response.body.items.item[i].category ||
                    "REH" == data.response.body.items.item[i].category ||
                    "RN1" == data.response.body.items.item[i].category ||
                    "SKY" == data.response.body.items.item[i].category ||
                    "T1H" == data.response.body.items.item[i].category
                    )
                {
                }
                else
                {
                    outIndex.Add(i);
                }
            }

            for (int j = outIndex.Count - 1; j >= 0; j--)
            {
                data.response.body.items.item.RemoveAt(outIndex[j]);
            }
            return data;
        }

        static public double GetObsrValueFromCategory(ForecastGribResult data, string category)
        {
            for (int i = 0; i < data.response.body.items.item.Count; i++)
            {
                if (category == data.response.body.items.item[i].category)
                {
                    return data.response.body.items.item[i].obsrValue;
                }
            }

            return -1;
        }

        static public string GetStartDateTime(ForecastTimeSpaceResult data)
        {
            return data.response.body.items.item[0].fcstDate.ToString() + data.response.body.items.item[0].fcstTime.ToString();
        }

        static public string GetStartDateTimePlusOneHour(string startDateTimeVal)
        {
            DateTime startDateTime = DateTime.ParseExact(startDateTimeVal, "yyyyMMddHHmm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime resultDateTime = startDateTime.AddHours(1);

            string tmpResult = resultDateTime.ToString("yyyyMMddHHmm");

            return tmpResult;
        }

        static public string GetStartDateTimePlusTwoHour(string startDateTimeVal)
        {
            DateTime startDateTime = DateTime.ParseExact(startDateTimeVal, "yyyyMMddHHmm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime resultDateTime = startDateTime.AddHours(2);

            string tmpResult = resultDateTime.ToString("yyyyMMddHHmm");

            return tmpResult;
        }

        static public string GetStartDateTimePlusThreeHour(string startDateTimeVal)
        {
            DateTime startDateTime = DateTime.ParseExact(startDateTimeVal, "yyyyMMddHHmm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime resultDateTime = startDateTime.AddHours(3);

            string tmpResult = resultDateTime.ToString("yyyyMMddHHmm");

            return tmpResult;
        }

        static public Item2 GetFcstWeatherFromTimeNCategory(ForecastTimeSpaceResult data, string date, string time, string category)
        {
            for (int i = 0; i < data.response.body.items.item.Count; i++)
            {
                if (category == data.response.body.items.item[i].category &&
                    date == data.response.body.items.item[i].fcstDate.ToString() &&
                    time == data.response.body.items.item[i].fcstTime.ToString())
                {
                    return data.response.body.items.item[i];
                }
            }

            return null;
        }

        static public string GetTomorrowDate()
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = now.AddDays(1);
            return tomorrow.ToString("yyyyMMdd");
        }

        static public string GetAfterTomorrowDate()
        {
            DateTime now = DateTime.Now;
            DateTime afterTomorrow = now.AddDays(2);
            return afterTomorrow.ToString("yyyyMMdd");
        }

        static public List<Item2> GetFcstSpaceFromDateNCategory(ForecastTimeSpaceResult data, string date, string category)
        {
            List<Item2> result = new List<Item2>();

            int tmpDataCount = 0;

            try
            {
                tmpDataCount = data.response.body.items.item.Count;
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine("NullReferenceException : {0}", e.Message);
                L4Logger l4Logger = new L4Logger("common.log");
                l4Logger.Add("System.NullReferenceException!");
                l4Logger.Close();
                return null;
            }

            for (int i = 0; i < data.response.body.items.item.Count; i++)
            {
                if (category == data.response.body.items.item[i].category &&
                    date == data.response.body.items.item[i].fcstDate.ToString())
                {
                    result.Add(data.response.body.items.item[i]);
                }
            }

            return result;
        }

        static public string ConvertCrawlTerm(string crawlTerm)
        {
            string result = null;
            switch (crawlTerm)
            {
                case "1H" :
                    result = "1시간";
                    break;
                case "30M" :
                    result = "30분";
                    break;
                case "1M" :
                    result = "1분";
                    break;
                default :
                    break;
            }
            return result;
        }

        static public string ConvertCrawlStatus(string crawlStatus)
        {
            string result = null;
            switch (crawlStatus)
            {
                case "R" :
                    result = "수집중";
                    break;
                case "S" :
                    result = "정지됨";
                    break;
                default :
                    break;
            }
            return result;
        }

        static public void DeleteLogFile(string fileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + fileName; //실행폴더 아래에 Log폴더

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                L4Logger l4Logger = new L4Logger("common.log");
                l4Logger.Add(FilePath + " is deleted");
                l4Logger.Close();
            }
        }
    }
}
