using System;
using System.Reflection;
using SchedulerLibrary.Interfaces;
using Quartz;
using System.Linq;
using System.Threading.Tasks;
using SchedulerLibrary.REST;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.FileExtensions;
using SchedulerLibrary.Repositories.Concrete;
using SchedulerLibrary.Entities;
using System.IO;
using System.Collections;
namespace SchedulerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appConfig.json");

            IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appConfig.json", true, true)
    .Build();






            //var types = Assembly.Load("SchedulerLibrary").GetTypes()
            //    .Where(x=>x.Namespace=="SchedulerLibrary.Jobs" && x.Name.EndsWith("Job"));


            //foreach (Type t in types)
            //{

            //        var myClass = Activator.CreateInstance(t);
            //        var tsk = (Task)t.GetMethod("Start").Invoke(myClass, null);


            //}



            // Testing Fogbugz Login Request

            // var fb = FogbugzRequest.GetInstance();

            //  var people = fb.getFogBugzItems(new FogBugzPostObject()
            //                                              {
            //                                                  Operation =(int)FogbugzRequest.Operation.ListFogBugzUsers,
            //                                                  payload=new {token=fb.Token}    
            //                                              }
            //                                  )
            //                                  .Cast<FogBugzUsers>();

            // var project = fb.getFogBugzItems(new FogBugzPostObject()
            // {
            //     Operation = (int)FogbugzRequest.Operation.ListProjects,
            //     payload = new { token = fb.Token }

            // }).Cast<FogBugzProjects>();

            //var singlePerson=  people.Where(p => p.FullName == "Amir Saleem").FirstOrDefault<FogBugzUsers>();

            FogBugzTicketRepository fr = new FogBugzTicketRepository();
            fr.Insert(new FogBugzTickets { TicketTitle = "ttt", TicketContent = "hhh", MileStone = 1, PersonAssignedTo = 23, TicketProject = "trst" });
            fr.Insert(new MessageLog { LogMessage = "hello", createdDate = DateTime.Now });

            Console.WriteLine("The Job's have been started. Press any key to exit.");
             Console.ReadLine();

        }


       
           
        }
    }

