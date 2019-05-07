using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Repositories.Abstract;
using SchedulerLibrary.Entities;
using SchedulerLibrary.Interfaces;
using SchedulerLibrary.EntityFramework.DbContexts;
using System.Linq;
namespace SchedulerLibrary.Repositories.Concrete
{
    public class FogBugzTicketRepository:IRepository
    {
        JobDbContext d;

        public FogBugzTicketRepository()
        {
            this.d = new JobDbContext();
        }
        public int Insert(iFogBugz item)
        {
           
            d.Add<FogBugzTickets>((FogBugzTickets)item);
            return d.SaveChanges();
        }

        public List<iFogBugz> getItems()
        {

            return d.Tickets.Where(x => ((x.lastRunDate==null && x.nextRunDate==null) ||(Convert.ToDateTime(x.nextRunDate).ToShortDateString()==DateTime.Now.ToShortDateString())) &&(x.NextRunType=="day" || x.NextRunType=="months")).ToList<iFogBugz>(); ;
        }

        public int Update(iFogBugz updatedItem)
        {
         d.Update<FogBugzTickets>((FogBugzTickets)updatedItem);
            return d.SaveChanges();
        }
    }
}
