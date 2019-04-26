using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Entities
{
    public class FogBugzPostObject
    {
     
       public  int Operation { get; set; }
        public string Command {
            get
            {
                if (Operation == 0)
                    return "logon";
               else if (Operation == 1)
                    return "new";
               else if (Operation == 2)
                    return "listPeople";
                else
                    return "listProjects";
            }

        }
        
        public object payload { get; set; }
    }
}
