using Quartz;
using Quartz.Impl;
using System;

namespace RatesMarkettingMailAutomation
{
    public class RatesMarkettingService
    {
        public RatesMarkettingService()
        {
        }

        public void OnStart()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<RatesMarkettingJob>().Build();
        
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("RatesMarkettingJob", "Marketting Mails")
            .WithCronSchedule("0 */1 * * * ?") //0 0 17 ? * MON-FRI to trigger every monday through friday at 5pm
            .StartAt(DateTime.UtcNow)
            .WithPriority(1)
            .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public void OnStop()
        {
        }
    }
}
