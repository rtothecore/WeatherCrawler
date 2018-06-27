using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class MsrstnInfoInqireSvrVo
    {
        public string _returnType;
        public string addr;
        public string districtNum;
        public string dmX;
        public string dmY;
        public string item;
        public string mangName;
        public string map;
        public string numOfRows;
        public string oper;
        public string pageNo;
        public string photo;
        public string resultCode;
        public string resultMsg;
        public int rnum;
        public string serviceKey;
        public string sggName;
        public string sidoName;
        public string stationCode;
        public string stationName;
        public double tm;
        public string tmX;
        public string tmY;
        public string totalCount;
        public string umdName;
        public string ver;
        public string vrml;
        public string year;
    }

    public class AirDataMetaResult
    {
        public MsrstnInfoInqireSvrVo MsrstnInfoInqireSvrVo;
        public List<MsrstnInfoInqireSvrVo> list;
        public MsrstnInfoInqireSvrVo parm;
        public int totalCount;
    }
}
