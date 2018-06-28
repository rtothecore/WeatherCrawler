using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class Result
    {
        public string postcode5;
        public string postcode6;
        public string ko_common;
        public string ko_doro;
        public string ko_jibeon;
        public string en_common;
        public string en_doro;
        public string en_jibeon;
        public string building_id;
        public string building_name;
        public string building_nums;
        public string other_addresses;
        public string road_id;
        public string internal_id;
    }

    public class SearchedAddressResult
    {
        public string version;
        public string error;
        public string msg;
        public int count;
        public string time;
        public string lang;
        public string sort;
        public string type;
        public string nums;
        public string cache;
        public List<Result> results;
    }
}
