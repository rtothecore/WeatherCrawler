using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeatherCrawler
{
    class HttpClientManager
    {
        private const string ServiceKey = "LjH8nbo4EHGuEhjqKt5sZaaSwUf%2BIMq9eBqk3pmPZ2lvUMEZ4Y%2Fqr4srvS20tgdFbAzf09sKOq5Ct8vielNKlg%3D%3D";
        private const string ServiceKeyForAir = "73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D";

        public HttpClientManager()
        {
            
        }

        public async Task<bool> RunGetGPSAndConvertNxNyAndWriteFI(string url, string urlParameters, List<string> addresses)
        {
            foreach(var address in addresses)
            {
                // https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/concepts/async/
                GoogleMapsResult gmr = await GetGPSLocationAsync(url, urlParameters + address);

                NxNyManager nnm = new NxNyManager();
                Coords nxny = nnm.Dfs_xy_conv("toXY", gmr.results[0].geometry.location.lat, gmr.results[0].geometry.location.lng);
                Console.WriteLine("Address:" + address +
                                  ", LatLng:(" + gmr.results[0].geometry.location.lat + ", " + gmr.results[0].geometry.location.lng + ")" +
                                  ", NxNy:(" + nxny.x + ", " + nxny.y + ")"
                                  );
                FwjournalIniManager fiManager = new FwjournalIniManager();
                fiManager.WriteAddress(address, nxny.x, nxny.y);
            }

            return true;
        }

        public async Task<bool> RunGetGPSAndConvertNxNyAndWriteAddr(string url, string urlParameters, string address)
        {
            GoogleMapsResult gmr = await GetGPSLocationAsync(url, urlParameters + address);

            NxNyManager nnm = new NxNyManager();
            Coords nxny = nnm.Dfs_xy_conv("toXY", gmr.results[0].geometry.location.lat, gmr.results[0].geometry.location.lng);
            Console.WriteLine("Address:" + address +
                                ", LatLng:(" + gmr.results[0].geometry.location.lat + ", " + gmr.results[0].geometry.location.lng + ")" +
                                ", NxNy:(" + nxny.x + ", " + nxny.y + ")"
                                );
            AddressIniManager aiManager = new AddressIniManager();
            aiManager.WriteAddress(address, nxny.x, nxny.y);

            return true;
        }

        // http://maps.googleapis.com/maps/api/geocode/json
        // ?sensor=false&language=ko&address=제주특별자치도%20제주시%20이도이동
        private static async Task<GoogleMapsResult> GetGPSLocationAsync(string url, string urlParameters)
        {
            string page = url + urlParameters;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    return await content.ReadAsAsync<GoogleMapsResult>();
                }
            }
        }

        public async Task<ForecastGribResult> RunGetForecastGrib(string url, string nx, string ny)
        {
            return await GetForecastGrib(url, nx, ny);            
        }

        /*
         * http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastGrib?
            serviceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D
            &base_date=20180625
            &base_time=1600
            &nx=60
            &ny=127
            &numOfRows=500
            &pageSize=500
            &pageNo=1
            &startPage=1
            &_type=json
         * 
         */
        private static async Task<ForecastGribResult> GetForecastGrib(string url, string nx, string ny)
        {
            string baseDate = UtilManager.GetBaseDateTimeForForecastGrib().Substring(0, 8);
            string baseTime = UtilManager.GetBaseDateTimeForForecastGrib().Substring(8, 4);
            string page = url +
                          "serviceKey=" + ServiceKey +
                          "&base_date=" + baseDate +
                          "&base_time=" + baseTime +
                          "&nx=" + nx +
                          "&ny=" + ny +
                          "&numOfRows=500" +
                          "&pageSize=500" +
                          "&pageNo=1" +
                          "&startPage=1" +
                          "&_type=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    // return await content.ReadAsAsync<ForecastGribResult>();
                    // https://www.newtonsoft.com/json
                    var stringResult = await content.ReadAsStringAsync();
                    ForecastGribResult result = JsonConvert.DeserializeObject<ForecastGribResult>(stringResult);
                    return result;
                }
            }
        }

        public async Task<AirDataResult> RunGetAirData(string url, string umd)
        {
            return await GetAirData(url, umd);
        }

        /*
         * http://openapi.airkorea.or.kr/openapi/services/rest/MsrstnInfoInqireSvc/getTMStdrCrdnt?
           umdName=일도이동
           &pageNo=1
           &numOfRows=10
           &ServiceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D
           &_returnType=json
         * 
         */
        private static async Task<AirDataResult> GetAirData(string url, string umd)
        {
            string page = url +
                          "umdName=" + umd +
                          "&pageNo=1" +
                          "&numOfRows=10" +
                          "&ServiceKey=" + ServiceKeyForAir +
                          "&_returnType=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    // TmX, TmY 조회
                    var stringResult = await content.ReadAsStringAsync();
                    AirDataMetaResult resultTmXY = JsonConvert.DeserializeObject<AirDataMetaResult>(stringResult);

                    // 측정소명 조회
                    AirDataMetaResult resultStationName = await GetStationName("http://openapi.airkorea.or.kr/openapi/services/rest/MsrstnInfoInqireSvc/getNearbyMsrstnList?",
                                                                 resultTmXY.list[0].tmX, resultTmXY.list[0].tmY);

                    // 측정값 조회
                    AirDataResult result = await GetAirMeasure("http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getMsrstnAcctoRltmMesureDnsty?",
                                                                resultStationName.list[0].stationName);

                    return result;

                }
            }
        }

        /*
         * http://openapi.airkorea.or.kr/openapi/services/rest/MsrstnInfoInqireSvc/getNearbyMsrstnList?
         * tmX=157273.257218
         * &tmY=1213.917415
         * &pageNo=1
         * &numOfRows=10
         * &ServiceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D
         * &_returnType=json
         * 
         */
        private static async Task<AirDataMetaResult> GetStationName(string url, string tmX, string tmY)
        {
            string page = url +
                          "tmX=" + tmX +
                          "&tmY=" + tmY +
                          "&pageNo=1" +
                          "&numOfRows=10" +
                          "&ServiceKey=" + ServiceKeyForAir +
                          "&_returnType=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    var stringResult = await content.ReadAsStringAsync();
                    AirDataMetaResult result = JsonConvert.DeserializeObject<AirDataMetaResult>(stringResult);
                    return result;
                }
            }
        }

        /*
         * http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getMsrstnAcctoRltmMesureDnsty?
		   numOfRows=10
		   &pageNo=1
		   &stationName=이도동
		   &dataTerm=DAILY
		   &ver=1.3
		   &ServiceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w
		   &_returnType=json
         * 
         */
        private static async Task<AirDataResult> GetAirMeasure(string url, string stationName)
        {
            string page = url +
                          "numOfRows=10" +
                          "&pageNo=1" +
                          "&stationName=" + stationName +
                          "&dataTerm=DAILY" +
                          "&ver=1.3" +
                          "&ServiceKey=" + ServiceKeyForAir +
                          "&_returnType=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    var stringResult = await content.ReadAsStringAsync();
                    AirDataResult result = JsonConvert.DeserializeObject<AirDataResult>(stringResult);
                    return result;
                }
            }
        }

        public async Task<ForecastTimeSpaceResult> RunGetForecastTime(string url, string nx, string ny)
        {
            return await GetForecastTime(url, nx, ny);            
        }

        /*
         *  http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastTimeData?
         *  serviceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D
         *  &base_date=20180626
         *  &base_time=1000
         *  &nx=60
         *  &ny=127
         *  &numOfRows=500
         *  &pageSize=10
         *  &pageNo=1
         *  &startPage=1
         *  &_type=json 
         * 
         */
        private static async Task<ForecastTimeSpaceResult> GetForecastTime(string url, string nx, string ny)
        {
            string baseDate = UtilManager.GetBaseDateTimeForForecastTime().Substring(0, 8);
            string baseTime = UtilManager.GetBaseDateTimeForForecastTime().Substring(8, 4);
            string page = url +
                          "serviceKey=" + ServiceKey +
                          "&base_date=" + baseDate +
                          "&base_time=" + baseTime +
                          "&nx=" + nx +
                          "&ny=" + ny +
                          "&numOfRows=500" +
                          "&pageSize=500" +
                          "&pageNo=1" +
                          "&startPage=1" +
                          "&_type=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    var stringResult = await content.ReadAsStringAsync();
                    ForecastTimeSpaceResult result = JsonConvert.DeserializeObject<ForecastTimeSpaceResult>(stringResult);
                    return result;
                }
            }
        }

        public async Task<ForecastTimeSpaceResult> RunGetForecastSpace(string url, string nx, string ny)
        {
            return await GetForecastSpace(url, nx, ny);
        }

        /*
         * http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastSpaceData?
         * serviceKey=73Jjl5lZRvBRKkGsPnGmZ7EL9JtwsWNi3hhCIN8cpVJzMdRRgyzntwz2lHmTKeR1tp7NWzoihNGGazcDEFgh8w%3D%3D
         * &base_date=20180626
         * &base_time=0500
         * &nx=60
         * &ny=127
         * &numOfRows=500
         * &pageSize=500
         * &pageNo=1
         * &startPage=1
         * &_type=json 
         * 
         */
        private static async Task<ForecastTimeSpaceResult> GetForecastSpace(string url, string nx, string ny)
        {
            string baseDate = UtilManager.GetBaseDateTimeForForecastTime().Substring(0, 8);
            string baseTime = UtilManager.GetBaseDateTimeForForecastTime().Substring(8, 4);
            string page = url +
                          "serviceKey=" + ServiceKey +
                          "&base_date=" + baseDate +
                          "&base_time=0500" +
                          "&nx=" + nx +
                          "&ny=" + ny +
                          "&numOfRows=500" +
                          "&pageSize=500" +
                          "&pageNo=1" +
                          "&startPage=1" +
                          "&_type=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    var stringResult = await content.ReadAsStringAsync();
                    ForecastTimeSpaceResult result = JsonConvert.DeserializeObject<ForecastTimeSpaceResult>(stringResult);
                    return result;
                }
            }
        }

        public async Task<SearchedAddressResult> RunGetSearchedAddress(string url, string searchText)
        {
            return await GetSearchedAddress(url, searchText);
        }

        /* https://api.poesis.kr/post/search.php?
         * q=검색어
         * &v=버전
         * &ref=도메인
         * 
         */
        private static async Task<SearchedAddressResult> GetSearchedAddress(string url, string searchText)
        {
            string version = "3.0.0-fwjournal";
            string domain = "www.ezinfotech.co.kr";
            string page = url +
                          "q=" + searchText +
                          "&v=" + version +
                          "&ref=" + domain;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    var stringResult = await content.ReadAsStringAsync();
                    SearchedAddressResult result = JsonConvert.DeserializeObject<SearchedAddressResult>(stringResult);
                    return result;
                }
            }
        }
    }
}
