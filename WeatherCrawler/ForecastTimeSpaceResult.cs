using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class Item2
    {
        public int baseDate;
        public int baseTime;
        public string category;
        public string fcstDate;
        public string fcstTime;
        public double fcstValue;
        public int nx;
        public int ny;
    }

    public class Items2
    {
        public List<Item2> item;
    }

    public class Body2
    {
        public Items2 items;
        public int numOfRows;
        public int pageNo;
        public int totalCount;
    }

    public class Header2
    {
        public string resultCode;
        public string resultMsg;
    }

    public class Response2
    {
        public Header2 header;
        public Body2 body;
    }

    public class ForecastTimeSpaceResult
    {
        public Response2 response;
    }
}
