using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Entities
{
   public class FogBugzTickets:iFogBugz
    {
        [Key]
       public int TicketId { get; set; }
        [MaxLength(200)]
       public string TicketTitle { get; set; }
        [MaxLength(500)]
        public string TicketContent { get; set; }

        [MaxLength(500)]
        public string TicketProject { get; set; }

        public int? PersonAssignedTo { get; set; }

        public int? MileStone { get; set; }

        public DateTime? lastRunDate { get; set; }

       
        public DateTime?  nextRunDate { get; set; }

        public int? NextScheduleInterval { get; set; }

        public string NextRunType { get; set; }
    }
}
