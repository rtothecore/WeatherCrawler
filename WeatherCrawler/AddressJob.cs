using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    class AddressJob : IJob
    {
        static private Object ThisLock = new Object();

        public virtual Task Execute(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;

            // 로그
            lock (ThisLock)
            {
                L4Logger l4Logger = new L4Logger(jobKey.Name + ".log");
                l4Logger.Add("Start Task : " + jobKey.Name);
                l4Logger.Close();
            }

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string address = dataMap.GetString("address");
            string nx = dataMap.GetString("nx");
            string ny = dataMap.GetString("ny");
            
            WeatherCrawlerData wcd = new WeatherCrawlerData();

            // db.ini 파일을 읽어서 수집서버 DB 연결
            DbIniManager dbIniManager = new DbIniManager();
            dbIniManager.ReadIni();
            DbManager dm = new DbManager(dbIniManager.IpAddress, dbIniManager.DbName, dbIniManager.CollectionName, dbIniManager.Id, dbIniManager.Pw);
            if (dm.Connect())
            {
                // 이미 같은 주소의 데이터가 있는지 체크
                if (dm.IsExistAddress(address))
                {
                    Console.WriteLine("이미 같은 주소의 데이터가 DB에 존재합니다");
                    // 기존 데이터 중 currentData만 보존
                    List<CurrentData> existCD = dm.GetCurrentData(address);
                    // 기존 데이터 삭제
                    dm.DeleteDocumentByAddress(address);
                    // 기존 currentData를 포함하여 데이터 생성
                    wcd = GetAssembledWCD(address, nx, ny, true, existCD);
                    // 수집서버 DB에 데이터 INSERT
                    dm.InsertWeatherData(wcd);
                }
                else
                {
                    Console.WriteLine("같은 주소의 데이터가 DB에 존재하지 않습니다");
                    wcd = GetAssembledWCD(address, nx, ny, false, null);

                    // 수집서버 DB에 데이터 INSERT
                    dm.InsertWeatherData(wcd);
                }
            }
            else
            {
                MessageBox.Show("수집서버 DB에 접속할 수 없습니다");
            }

            return Task.FromResult(0);
        }

        private async Task<ForecastGribResult> RunHCMForForecastGrib(string nx, string ny)
        {
            // 초단기실황조회
            HttpClientManager hcManager = new HttpClientManager();
            return await hcManager.RunGetForecastGrib("http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastGrib?", nx, ny);
        }

        private async Task<AirDataResult> RunHCMAirData(string umd)
        {
            // 대기 정보 조회
            HttpClientManager hcManager = new HttpClientManager();
            return await hcManager.RunGetAirData("http://openapi.airkorea.or.kr/openapi/services/rest/MsrstnInfoInqireSvc/getTMStdrCrdnt?", umd);
        }

        private async Task<ForecastTimeSpaceResult> RunHCMForForecastTime(string nx, string ny)
        {
            // 초단기예보조회
            HttpClientManager hcManager = new HttpClientManager();
            return await hcManager.RunGetForecastTime("http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastTimeData?", nx, ny);
        }

        private async Task<ForecastTimeSpaceResult> RunHCMForForecastSpace(string nx, string ny)
        {
            // 동네예보조회
            HttpClientManager hcManager = new HttpClientManager();
            return await hcManager.RunGetForecastSpace("http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastSpaceData?", nx, ny);
        }

        private WeatherCrawlerData GetAssembledWCD(string address, string nx, string ny, bool isExistCD, List<CurrentData> existCD)
        {
            // 데이터 수집
            var resultForForecastGrib = RunHCMForForecastGrib(nx, ny);
            var resultForAirData = RunHCMAirData(UtilManager.GetUmdFromAddress(address));
            var resultForForecastTime = RunHCMForForecastTime(nx, ny);
            var resultForForecastSpace = RunHCMForForecastSpace(nx, ny);

            // 데이터 생성
            WeatherCrawlerData wcd = new WeatherCrawlerData();
            wcd.address = address;
            wcd.nx = Int32.Parse(nx);
            wcd.ny = Int32.Parse(ny);

            // currentData
            ForecastGribResult reassembledFGR = UtilManager.ReassembleDataForForecastGrib(resultForForecastGrib.Result);
            CurrentData tmpCD = new CurrentData();
            tmpCD.insertDate = UtilManager.GetNowDateTime();
            tmpCD.weather = new Weather();
            tmpCD.weather.baseDate = reassembledFGR.response.body.items.item[0].baseDate.ToString();
            tmpCD.weather.baseTime = reassembledFGR.response.body.items.item[0].baseTime;
            tmpCD.weather.pty = UtilManager.GetObsrValueFromCategory(reassembledFGR, "PTY");
            tmpCD.weather.reh = UtilManager.GetObsrValueFromCategory(reassembledFGR, "REH");
            tmpCD.weather.rn1 = UtilManager.GetObsrValueFromCategory(reassembledFGR, "RN1");
            tmpCD.weather.sky = UtilManager.GetObsrValueFromCategory(reassembledFGR, "SKY");
            tmpCD.weather.t1h = UtilManager.GetObsrValueFromCategory(reassembledFGR, "T1H");

            AirDataResult adr = resultForAirData.Result;
            tmpCD.air = new Air();
            tmpCD.air.dataTime = adr.list[0].dataTime;
            if ("-" == adr.list[0].so2Value)
                tmpCD.air.so2Value = -1;
            else
                tmpCD.air.so2Value = Double.Parse(adr.list[0].so2Value);
            if ("-" == adr.list[0].coValue)
                tmpCD.air.coValue = -1;
            else
                tmpCD.air.coValue = Double.Parse(adr.list[0].coValue);
            if ("-" == adr.list[0].o3Value)
                tmpCD.air.o3Value = -1;
            else
                tmpCD.air.o3Value = Double.Parse(adr.list[0].o3Value);
            if ("-" == adr.list[0].no2Value)
                tmpCD.air.no2Value = -1;
            else
                tmpCD.air.no2Value = Double.Parse(adr.list[0].no2Value);
            if ("-" == adr.list[0].pm10Value)
                tmpCD.air.pm10Value = -1;
            else
                tmpCD.air.pm10Value = Double.Parse(adr.list[0].pm10Value);
            if ("-" == adr.list[0].pm25Value)
                tmpCD.air.pm25Value = -1;
            else
                tmpCD.air.pm25Value = Double.Parse(adr.list[0].pm25Value);

            wcd.currentData = new List<CurrentData>();
            if (isExistCD)
            {
                wcd.currentData = existCD;
            }
            wcd.currentData.Add(tmpCD);

            // twoHour
            ForecastTimeSpaceResult ftsr = resultForForecastTime.Result;
            Fcst tmpFcst = new Fcst();
            tmpFcst.insertDate = UtilManager.GetNowDateTime();

            string startDateTime = UtilManager.GetStartDateTime(ftsr);
            string startDate = startDateTime.Substring(0, 8);
            string startTime = startDateTime.Substring(8, 4);

            Item2 tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, startDate, startTime, "PTY");
            tmpFcst.weather = new List<FcstWeather>();
            FcstWeather tmpFW = new FcstWeather();
            tmpFW.fcstDate = tmpItem.fcstDate.ToString();
            tmpFW.fcstTime = tmpItem.fcstTime.ToString();
            tmpFW.pty = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, startDate, startTime, "RN1");
            tmpFW.rn1 = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, startDate, startTime, "SKY");
            tmpFW.sky = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, startDate, startTime, "T1H");
            tmpFW.t1h = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, startDate, startTime, "REH");
            tmpFW.reh = tmpItem.fcstValue;
            tmpFcst.weather.Add(tmpFW);

            string startDatePlusOneHour = UtilManager.GetStartDateTimePlusOneHour(startDateTime);
            string plusOneHourDate = startDatePlusOneHour.Substring(0, 8);
            string plusOneHourTime = startDatePlusOneHour.Substring(8, 4);

            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, plusOneHourDate, plusOneHourTime, "PTY");
            tmpFW = new FcstWeather();
            tmpFW.fcstDate = tmpItem.fcstDate.ToString();
            tmpFW.fcstTime = tmpItem.fcstTime.ToString();
            tmpFW.pty = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, plusOneHourDate, plusOneHourTime, "RN1");
            tmpFW.rn1 = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, plusOneHourDate, plusOneHourTime, "SKY");
            tmpFW.sky = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, plusOneHourDate, plusOneHourTime, "T1H");
            tmpFW.t1h = tmpItem.fcstValue;
            tmpItem = UtilManager.GetFcstWeatherFromTimeNCategory(ftsr, plusOneHourDate, plusOneHourTime, "REH");
            tmpFW.reh = tmpItem.fcstValue;
            tmpFcst.weather.Add(tmpFW);

            wcd.twoHour = tmpFcst;

            // tomorrow
            ftsr = resultForForecastSpace.Result;
            Fcst2 tmpFcst2 = new Fcst2();
            tmpFcst2.insertDate = UtilManager.GetNowDateTime();

            string tomorrowDate = UtilManager.GetTomorrowDate();

            List<Item2> tmpItemsPTY = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, tomorrowDate, "PTY");
            List<Item2> tmpItemsR06 = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, tomorrowDate, "R06");
            List<Item2> tmpItemsSKY = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, tomorrowDate, "SKY");
            List<Item2> tmpItemsT3H = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, tomorrowDate, "T3H");
            List<Item2> tmpItemsREH = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, tomorrowDate, "REH");

            tmpFcst2.weather = new List<FcstWeather2>();
            for (int i = 0; i < tmpItemsPTY.Count; i++)
            {
                FcstWeather2 tmpFW2 = new FcstWeather2();
                tmpFW2.fcstDate = tmpItemsPTY[i].fcstDate;
                tmpFW2.fcstTime = tmpItemsPTY[i].fcstTime;
                tmpFW2.pty = tmpItemsPTY[i].fcstValue;
                if (0 == i % 2)
                {
                    tmpFW2.r06 = tmpItemsR06[i / 2].fcstValue;
                }
                tmpFW2.sky = tmpItemsSKY[i].fcstValue;
                tmpFW2.t3h = tmpItemsT3H[i].fcstValue;
                tmpFW2.reh = tmpItemsREH[i].fcstValue;
                tmpFcst2.weather.Add(tmpFW2);
            }

            wcd.tomorrow = tmpFcst2;

            // afterTomorrow
            tmpFcst2 = new Fcst2();
            tmpFcst2.insertDate = UtilManager.GetNowDateTime();
            string afterTomorrowDate = UtilManager.GetAfterTomorrowDate();

            tmpItemsPTY = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, afterTomorrowDate, "PTY");
            tmpItemsR06 = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, afterTomorrowDate, "R06");
            tmpItemsSKY = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, afterTomorrowDate, "SKY");
            tmpItemsT3H = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, afterTomorrowDate, "T3H");
            tmpItemsREH = UtilManager.GetFcstSpaceFromDateNCategory(ftsr, afterTomorrowDate, "REH");

            tmpFcst2.weather = new List<FcstWeather2>();
            for (int i = 0; i < tmpItemsPTY.Count; i++)
            {
                FcstWeather2 tmpFW2 = new FcstWeather2();
                tmpFW2.fcstDate = tmpItemsPTY[i].fcstDate;
                tmpFW2.fcstTime = tmpItemsPTY[i].fcstTime;
                tmpFW2.pty = tmpItemsPTY[i].fcstValue;
                if (0 == i % 2)
                {
                    tmpFW2.r06 = tmpItemsR06[i / 2].fcstValue;
                }
                tmpFW2.sky = tmpItemsSKY[i].fcstValue;
                tmpFW2.t3h = tmpItemsT3H[i].fcstValue;
                tmpFW2.reh = tmpItemsREH[i].fcstValue;
                tmpFcst2.weather.Add(tmpFW2);
            }

            wcd.afterTomorrow = tmpFcst2;

            return wcd;
        }
    }
}
