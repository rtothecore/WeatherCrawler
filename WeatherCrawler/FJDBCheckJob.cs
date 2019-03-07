using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    class FJDBCheckJob : IJob
    {
        public virtual Task Execute(IJobExecutionContext context)
        {
            // fwjournal.ini 수집작업 모두 멈춤
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            IScheduler crawScheduler = (IScheduler)dataMap.Get("crawlScheduler");
            if (null != crawScheduler)
            {
                crawScheduler.PauseAll();
            }

            // 영농일지 DB에서 주소 얻기
            FwjournalIniManager fwjIniManager = new FwjournalIniManager();
            fwjIniManager.ReadIni();
            List<string> addresses = new List<string>();
            DbManager dm2 = new DbManager(fwjIniManager.IpAddress, fwjIniManager.DbName, fwjIniManager.CollectionName, fwjIniManager.Id, fwjIniManager.Pw);
            if (dm2.Connect())
            {
                addresses = dm2.ReadFwjournalLands();
            }
            else
            {
                MessageBox.Show("영농일지 DB에 접속할 수 없습니다");
            }

            // fwjournal.ini와 영농일지 DB의 주소(addresses)를 비교하여 크기가 다른 경우 fwjournal.ini 업데이트
            fwjIniManager.ReadAddress();

            if (addresses.Count > fwjIniManager.Addresses.Count ||
               addresses.Count < fwjIniManager.Addresses.Count)
            {
                // fwjournal.ini의 주소정보 모두 지움
                fwjIniManager.DeleteAddressLine();

                // 영농일지 주소 읽어서 fwjournal.ini에 주소 쓰기
                var result = RunGetGPSAndConvertNxNyAndWriteFI(addresses);
            }

            // fwjournal.ini 수집작업 모두 재가동
            if (null != crawScheduler)
            { 
                crawScheduler.ResumeAll();
            }

            return Task.FromResult(0);
        }

        private async Task<bool> RunGetGPSAndConvertNxNyAndWriteFI(List<string> addresses)
        {
            HttpClientManager hcManager = new HttpClientManager();
            /*
            return await hcManager.RunGetGPSAndConvertNxNyAndWriteFI("http://maps.googleapis.com/maps/api/geocode/json",
                                                        "?sensor=false&language=ko&address=",
                                                        addresses);
            */
            return await hcManager.RunGetGPSAndConvertNxNyAndWriteFI("https://maps.googleapis.com/maps/api/geocode/json",
                                                        "?address=",
                                                        addresses);
        }
    }
}
