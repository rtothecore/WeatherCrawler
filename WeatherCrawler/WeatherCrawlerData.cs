using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class FcstWeather
    {
        public string fcstDate;
        public string fcstTime;
        public double t1h;
        public double reh;
        public double rn1;
        public double pty;
        public double sky;
    }

    public class FcstWeather2
    {
        public string fcstDate;
        public string fcstTime;
        public double t3h;
        public double reh;
        public double r06;
        public double pty;
        public double sky;
    }

    public class Fcst
    {
        public string insertDate;
        public List<FcstWeather> weather;
    }

    public class Fcst2
    {
        public string insertDate;
        public List<FcstWeather2> weather;
    }

    public class Air
    {
        public string dataTime;
        public double so2Value;
        public double coValue;
        public double o3Value;
        public double no2Value;
        public double pm10Value;
        public double pm25Value;
    }

    public class Weather
    {
        public string baseDate;
        public string baseTime;
        public double t1h;
        public double reh;
        public double rn1;
        public double pty;
        public double sky;
    }

    public class CurrentData
    {
        public string insertDate;
        public Weather weather;
        public Air air;
    }

    public class WeatherCrawlerData
    {
        public string address;
        public int nx;
        public int ny;
        public List<CurrentData> currentData;
        public Fcst twoHour;
        public Fcst2 tomorrow;
        public Fcst2 afterTomorrow;
    }
}
