using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class ArpltnInforInqireSvcVo
    {
        public string _returnType;
        public string coGrade;
        public string coValue;
        public string dataTerm;
        public string dataTime;
        public string khaiGrade;
        public string khaiValue;
        public string mangName;
        public string no2Grade;
        public string no2Value;
        public string numOfRows;
        public string o3Grade;
        public string o3Value;
        public string pageNo;
        public string pm10Grade;
        public string pm10Grade1h;
        public string pm10Value;
        public string pm10Value24;
        public string pm25Grade;
        public string pm25Grade1h;
        public string pm25Value;
        public string pm25Value24;
        public string resultCode;
        public string resultMsg;
        public int rnum;
        public string serviceKey;
        public string sidoName;
        public string so2Grade;
        public string so2Value;
        public string stationCode;
        public string stationName;
        public string totalCount;
        public string ver;
    }

    public class AirDataResult
    {
        public List<ArpltnInforInqireSvcVo> list;
        public ArpltnInforInqireSvcVo parm;
        public ArpltnInforInqireSvcVo ArpltnInforInqireSvcVo;
        public int totalCount;
    }
}
