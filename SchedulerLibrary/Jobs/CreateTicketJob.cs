using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;
using Quartz.Impl;
namespace SchedulerLibrary.Jobs
{
    public class CreateTicketJob:IJob
    {
        public Task  Execute(IJobExecutionContext context)
        {

            return Task.Run(() =>
            {
                Console.WriteLine("This Job is Set to run Every 10 Seconds");
            });
            
            
        }

        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CreateTicketJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(10))
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
