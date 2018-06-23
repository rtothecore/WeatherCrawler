using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    class Coords
    {
        public double x;
        public double y;
        public double lat;
        public double lng;
    }

    class NxNyManager
    {
        // https://gist.github.com/fronteer-kr/14d7f779d52a21ac2f16
        const double RE = 6371.00877; // 지구 반경(km)
        const double GRID = 5.0; // 격자 간격(km)
        const double SLAT1 = 30.0; // 투영 위도1(degree)
        const double SLAT2 = 60.0; // 투영 위도2(degree)
        const double OLON = 126.0; // 기준점 경도(degree)
        const double OLAT = 38.0; // 기준점 위도(degree)
        const double XO = 43; // 기준점 X좌표(GRID)
        const double YO = 136; // 기1준점 Y좌표(GRID)

        public NxNyManager()
        {
        }

        //
        // LCC DFS 좌표변환 ( code : "toXY"(위경도->좌표, v1:위도, v2:경도), "toLL"(좌표->위경도,v1:x, v2:y) )
        //
        public Coords Dfs_xy_conv(string code, double v1, double v2)
        {
            double DEGRAD = Math.PI / 180.0;
            double RADDEG = 180.0 / Math.PI;

            double re = RE / GRID;
            double slat1 = SLAT1 * DEGRAD;
            double slat2 = SLAT2 * DEGRAD;
            double olon = OLON * DEGRAD;
            double olat = OLAT * DEGRAD;

            double sn = Math.Tan(Math.PI * 0.25 + slat2 * 0.5) / Math.Tan(Math.PI * 0.25 + slat1 * 0.5);
            sn = Math.Log(Math.Cos(slat1) / Math.Cos(slat2)) / Math.Log(sn);
            double sf = Math.Tan(Math.PI * 0.25 + slat1 * 0.5);
            sf = Math.Pow(sf, sn) * Math.Cos(slat1) / sn;
            double ro = Math.Tan(Math.PI * 0.25 + olat * 0.5);
            ro = re * sf / Math.Pow(ro, sn);
            Coords rs = new Coords();
            if (code == "toXY")
            {
                rs.lat = v1;
                rs.lng = v2;
                double ra = Math.Tan(Math.PI * 0.25 + (v1) * DEGRAD * 0.5);
                ra = re * sf / Math.Pow(ra, sn);
                double theta = v2 * DEGRAD - olon;
                if (theta > Math.PI) theta -= 2.0 * Math.PI;
                if (theta < -Math.PI) theta += 2.0 * Math.PI;
                theta *= sn;
                rs.x = Math.Floor(ra * Math.Sin(theta) + XO + 0.5);
                rs.y = Math.Floor(ro - ra * Math.Cos(theta) + YO + 0.5);
            }
            else
            {
                rs.x = v1;
                rs.y = v2;
                double xn = v1 - XO;
                double yn = ro - v2 + YO;
                double ra = Math.Sqrt(xn * xn + yn * yn);
                if (sn < 0.0) ra = -ra;
                double alat = Math.Pow((re * sf / ra), (1.0 / sn));
                alat = 2.0 * Math.Atan(alat) - Math.PI * 0.5;

                double theta = 0.0;
                if (Math.Abs(xn) <= 0.0)
                {
                    theta = 0.0;
                }
                else
                {
                    if (Math.Abs(yn) <= 0.0)
                    {
                        theta = Math.PI * 0.5;
                        if (xn < 0.0) theta = -theta;
                    }
                    else theta = Math.Atan2(xn, yn);
                }
                double alon = theta / sn + olon;
                rs.lat = alat * RADDEG;
                rs.lng = alon * RADDEG;
            }
            return rs;
        }
    }
}
