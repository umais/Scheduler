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

        FogbugzRequest fb ;
        FogBugzTicketRepository _repository;
        public CreateTicketJob()
        {
            fb = FogbugzRequest.GetInstance();
            _repository = new FogBugzTicketRepository();
        }
        public Task  Execute(IJobExecutionContext context)
        {

            return Task.Run(() =>
            {
                Console.WriteLine("This Job is Set to run Every 2 minutes ");
               
               LogMessage(new MessageLog() { LogMessage = "Job for Entering Tickets Started", createdDate = DateTime.Now });

                //In this Code we will get all rows from the Database for the Tickets and Create those tickets if Time is Due On them if Not then We will skip those tickets
                try
                {

                    
                    

                    var ticks = _repository.getTickets();
                    int totalTickets = 0;
                    foreach (FogBugzTickets t in ticks)
                    {

                        //Add The Tickets to Fogbugz 
                        int FogBugzId= CreateFbTicket(t);
                        //Log the Success
                        LogMessage(new MessageLog() { LogMessage = "Ticket Created in fogBugz with Id "+FogBugzId, createdDate = DateTime.Now });
                        totalTickets++;
                        //Update the Next Run Date.
                        UpdateNextRun(t);

                     

                    }
                    LogMessage(new MessageLog() { LogMessage = "Total tickets created " + totalTickets, createdDate = DateTime.Now });
                }
                catch(Exception ex)
                {
                    LogMessage(new MessageLog() { LogMessage = "Error Occured: "+ex.Message , createdDate = DateTime.Now });
                }
                finally
                {
                    LogMessage(new MessageLog() { LogMessage = "Job ended and waiting for Next Run " , createdDate = DateTime.Now });
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
            
            return fb.CreateNewCase(t);
     
        }

        private int LogMessage(MessageLog m)
        {
            Console.WriteLine(m.LogMessage);
           return _repository.Insert(m);
        }


        private int UpdateNextRun(FogBugzTickets t)
        {
            if (t.NextRunType == "day")
                t.nextRunDate = DateTime.Now.AddDays(Convert.ToDouble(t.NextScheduleInterval));
            if (t.NextRunType == "month")
                t.nextRunDate = DateTime.Now.AddMonths(Convert.ToInt32(t.NextScheduleInterval));
            t.lastRunDate = System.DateTime.Now;
            return _repository.Update(t);
        }
    }
}


/** This is a Comment
 * Every Month Cron Syntax:  0 0 0 ? * * *
 * Every Quarter 
 */
