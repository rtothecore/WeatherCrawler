using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class Item
    {
        public double baseDate;
        public string baseTime;
        public string category;
        public int nx;
        public int ny;
        public double obsrValue;
    }

    public class Items
    {
        public List<Item> item;
    }

    public class Body
    {
        public Items items;
        public int numOfRows;
        public int pageNo;
        public int totalCount;
    }

    public class Header
    {
        public string resultCode;
        public string resultMsg;
    }

    public class Response
    {
        public Header header;
        public Body body;
    }

    public class ForecastGribResult
    {
        public Response response;
    }
}
