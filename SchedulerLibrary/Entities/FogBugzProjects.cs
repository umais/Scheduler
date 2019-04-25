using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerLibrary.Entities
{
   public  class FogBugzProjects
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
