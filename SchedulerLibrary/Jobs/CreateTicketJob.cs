using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;
using Quartz.Impl;
using SchedulerLibrary.Entities;
using SchedulerLibrary.EntityFramework.DbContexts;
using SchedulerLibrary.Repositories.Concrete;
using SchedulerLibrary.REST;
namespace SchedulerLibrary.Jobs
{
    public class CreateTicketJob:IJob
    {
        public Task  Execute(IJobExecutionContext context)
        {

            return Task.Run(() =>
            {
                Console.WriteLine("This Job is Set to run Every 2 minutes ");

                //In this Code we will get all rows from the Database for the Tickets and Create those tickets if Time is Due On them if Not then We will skip those tickets

                FogBugzTicketRepository fb = new FogBugzTicketRepository();

                var ticks = fb.getItems();

                foreach (FogBugzTickets t in ticks)
                {

                    //Add The Tickets to Fogbugz 
                    CreateFbTicket(t);

                    //Log the Success

                    //Log the errors if any
                    
                    //Update the Next Run Date.

                    Console.WriteLine("Ticket name: " + t.TicketTitle);
                    if(t.NextRunType=="day")
                    t.nextRunDate = DateTime.Now.AddDays(Convert.ToDouble(t.NextScheduleInterval));
                    if(t.NextRunType=="month")
                        t.nextRunDate = DateTime.Now.AddMonths(Convert.ToInt32(t.NextScheduleInterval));
                    t.lastRunDate = System.DateTime.Now;
                        fb.Update(t);
                    
                }

                
            });
            
            
        }

        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CreateTicketJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                 .WithDailyTimeIntervalSchedule(s => s.WithIntervalInMinutes(1))
                // .WithCronSchedule("0 0 0 ? JAN,APR,JUL,OCT * *")
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }


        private int CreateFbTicket(FogBugzTickets t)
        {
            var fb = FogbugzRequest.GetInstance();
            return fb.CreateNewCase(t);
     
        }
    }
}


/** This is a Comment
 * Every Month Cron Syntax:  0 0 0 ? * * *
 * Every Quarter 
 */
