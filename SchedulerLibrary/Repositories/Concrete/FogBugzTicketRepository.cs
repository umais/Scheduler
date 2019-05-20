using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Repositories.Abstract;
using SchedulerLibrary.Entities;
using SchedulerLibrary.Interfaces;
using SchedulerLibrary.EntityFramework.DbContexts;
using System.Linq;
using System.Reflection;
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

            int returnValue = -1;
            try
            {


                Type t = item.GetType();
                MethodInfo[] method = d.GetType().GetMethods();
                MethodInfo genericmethod = method.FirstOrDefault(m => m.IsGenericMethod && m.Name == "Add").MakeGenericMethod(t);
                genericmethod.Invoke(d, new object[] { item });
                returnValue = d.SaveChanges();
            }
            catch(Exception ex)
            {
                //For now throwing the Exception but will make sure to log the information in a file to get easy access to error messages.
                throw ex;
            }
            return returnValue;

        }

        public List<FogBugzTickets> getTickets()
        {

            return d.Tickets.Where(x => (
                                             (x.lastRunDate==null && x.nextRunDate==null) ||
                                            (Convert.ToDateTime(x.nextRunDate).ToShortDateString()==DateTime.Now.ToShortDateString()   &&
                                               (x.NextRunType=="day" || x.NextRunType=="months" )) ||
                                               (x.NextRunType=="minute")
                                               
                                               ))
                                .ToList<FogBugzTickets>(); ;
        }

        public int Update(iFogBugz updatedItem)
        {

            Type t = updatedItem.GetType();
            MethodInfo[] method = d.GetType().GetMethods();
            MethodInfo genericmethod = method.FirstOrDefault(m => m.IsGenericMethod && m.Name == "Update").MakeGenericMethod(t);
            genericmethod.Invoke(d, new object[] { updatedItem });
            //d.Update<FogBugzTickets>((FogBugzTickets)updatedItem);

            return d.SaveChanges();
        }
    }
}
