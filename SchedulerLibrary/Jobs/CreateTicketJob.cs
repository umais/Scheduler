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
                .WithCronSchedule("0 0 0 ? JAN,APR,JUL,OCT * *")
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}


/** This is a Comment
 * Every Month Cron Syntax:  0 0 0 ? * * *
 * Every Quarter 
 */
