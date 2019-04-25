using System;
using System.Reflection;
using SchedulerLibrary.Interfaces;
using Quartz;
using System.Linq;
using System.Threading.Tasks;
using SchedulerLibrary.Entities;
using SchedulerLibrary.EntityFramework.DbContexts;
using SchedulerLibrary.REST;
namespace SchedulerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Assembly myAssembly = Assembly.GetExecutingAssembly();
            var types = Assembly.Load("SchedulerLibrary").GetTypes()
                .Where(x=>x.Namespace=="SchedulerLibrary.Jobs" && x.Name.EndsWith("Job"));


            foreach (Type t in types)
            {
                
                    var myClass = Activator.CreateInstance(t);
                    var tsk = (Task)t.GetMethod("Start").Invoke(myClass, null);
                
             
            }

          //  JobDbContext d = new JobDbContext();
            //d.Add<Tickets>(new Tickets() { TicketId = 23, TicketTitle = "Testing 123", TicketContent = "This is my content please check" });
            //d.SaveChanges();

            //Testing Fogbugz Login Request
            FogbugzRequest fb = new FogbugzRequest();

            var o = fb.FogBugzLogin("azureadministrator@aligncare.com", "Tw1stone23");
            var people = fb.getAllManuscriptUsers(o);
            
            Console.WriteLine("The Job's have been started.");
            Console.ReadLine();

        }
           
        }
    }

