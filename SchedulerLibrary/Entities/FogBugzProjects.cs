using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Entities
{
   public  class FogBugzProjects:iFogBugz
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
