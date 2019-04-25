using System;
using System.Reflection;
using SchedulerLibrary.Interfaces;
using Quartz;
using System.Linq;
using System.Threading.Tasks;
using SchedulerLibrary.Entities;
using SchedulerLibrary.EntityFramework.DbContexts;
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

            JobDbContext d = new JobDbContext();
            //d.Add<Tickets>(new Tickets() { TicketId = 23, TicketTitle = "Testing 123", TicketContent = "This is my content please check" });
            //d.SaveChanges();
            Console.WriteLine("The Job's have been started.");
            Console.ReadLine();

        }
           
        }
    }

