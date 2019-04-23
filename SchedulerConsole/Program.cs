using System;
using System.Reflection;
using SchedulerLibrary.Interfaces;
using Quartz;
using System.Linq;
using System.Threading.Tasks;
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
            Console.WriteLine("Now Enter Something");
            Console.ReadLine();

        }
           
        }
    }

