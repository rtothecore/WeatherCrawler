using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    class CrawlManager
    {
        // public static IScheduler scheduler = null;
        // public static ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
        public IScheduler schedulerForFJ = null;
        public IScheduler schedulerForAddr = null;

        public CrawlManager()
        {
            RunTasks();
        }

        // https://sites.google.com/site/netcorenote/scheduler-in-netcore/quartz/02--tutorial-of-quartz-in-netcore/01-simpleexamplewithquartznet300-alpha2
        public async Task TaskFJJob(string indexNo, string address, string nx, string ny, string crawlTerm, string crawlStatus)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            schedulerForFJ = schedulerFactory.GetScheduler().Result;
            schedulerForFJ.Start().Wait();

            int crawlTermMin = 0;
            switch(crawlTerm)
            {
                case "1H":
                    crawlTermMin = 60;
                    break;
                case "30M":
                    crawlTermMin = 30;
                    break;
                case "1M":
                    crawlTermMin = 1;
                    break;
                default:
                    break;
            }

            int ScheduleIntervalInMinute = crawlTermMin;            

            IJobDetail job = JobBuilder.Create<Job>().WithIdentity(indexNo)
                                                     .UsingJobData("address", address)
                                                     .UsingJobData("nx", nx)
                                                     .UsingJobData("ny", ny)
                                                     .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("JobTrigger" + indexNo)
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(ScheduleIntervalInMinute).RepeatForever())
                .Build();

            await schedulerForFJ.ScheduleJob(job, trigger);

            if ("S" == crawlStatus)
            {
                await schedulerForFJ.PauseJob(new JobKey(indexNo));
            }
            Console.WriteLine("TaskFJJob - job:{0}, trigger:{1}", job.ToString(), trigger.ToString());

            // 로그
            L4Logger l4Logger = new L4Logger(indexNo + ".log");
            l4Logger.Add("TaskFJJob - job:" + job.ToString() + ", trigger:" + trigger.ToString());
            l4Logger.Close();
        }

        public async Task TaskAddrJob(string indexNo, string address, string nx, string ny, string crawlTerm, string crawlStatus)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            schedulerForAddr = schedulerFactory.GetScheduler().Result;
            schedulerForAddr.Start().Wait();

            int crawlTermMin = 0;
            switch (crawlTerm)
            {
                case "1H":
                    crawlTermMin = 60;
                    break;
                case "30M":
                    crawlTermMin = 30;
                    break;
                case "1M":
                    crawlTermMin = 1;
                    break;
                default:
                    break;
            }

            int ScheduleIntervalInMinute = crawlTermMin;

            IJobDetail job = JobBuilder.Create<Job>().WithIdentity(indexNo)
                                                     .UsingJobData("address", address)
                                                     .UsingJobData("nx", nx)
                                                     .UsingJobData("ny", ny)
                                                     .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("JobTrigger" + indexNo)
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(ScheduleIntervalInMinute).RepeatForever())
                .Build();

            await schedulerForAddr.ScheduleJob(job, trigger);
            
            if ("S" == crawlStatus)
            {
                await schedulerForAddr.PauseJob(new JobKey(indexNo));
            }
            Console.WriteLine("TaskAddrJob - job:{0}, trigger:{1}", job.ToString(), trigger.ToString());

            // 로그
            L4Logger l4Logger = new L4Logger(indexNo + ".log");
            l4Logger.Add("TaskAddrJob - job:" + job.ToString() + ", trigger:" + trigger.ToString());
            l4Logger.Close();
        }

        public void RunTasks()
        {
            // fwjournal.ini 읽어서 수집시작
            FwjournalIniManager fIManager = new FwjournalIniManager();
            fIManager.ReadAddress();
            foreach (var address in fIManager.Addresses)
            {
                TaskFJJob(address.IndexNo, address.Address, address.Nx, address.Ny, address.CrawlTerm, address.CrawlStatus).GetAwaiter();
                // TaskFJJob(address.IndexNo, address.Address, address.CrawlTerm).GetAwaiter().GetResult();
            }

            // address.ini 읽어서 수집시작
            AddressIniManager aIManager = new AddressIniManager();
            aIManager.ReadAddress();
            foreach (var address in aIManager.Addresses)
            {
                TaskAddrJob(address.IndexNo, address.Address, address.Nx, address.Ny, address.CrawlTerm, address.CrawlStatus).GetAwaiter();
            }
        }
    }
}
