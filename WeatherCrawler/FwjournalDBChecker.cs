using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    class FwjournalDBChecker
    {
        IScheduler CrawlScheduler = null;

        public FwjournalDBChecker(IScheduler CrawlManagerScheduler)
        {
            CrawlScheduler = CrawlManagerScheduler;
            TaskFJJob().GetAwaiter();
        }

        public async Task TaskFJJob()
        {
            IScheduler scheduler = null;
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start().Wait();

            JobKey jobKey = JobKey.Create("0");

            // https://stackoverflow.com/questions/7137960/quartz-scheduler-how-to-pass-custom-objects-as-jobparameter
            JobDataMap jdm = new JobDataMap();
            jdm.Put("crawlScheduler", CrawlScheduler);

            IJobDetail job = JobBuilder.Create<FJDBCheckJob>().WithIdentity(jobKey)
                                                              .UsingJobData(jdm)
                                                              .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("JobTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
