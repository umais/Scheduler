using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Entities
{
    public class TicketsCreated:iFogBugz
    {
        [Key]
        public int FogBugzTicketId { get; set; }

        public int TicketId { get; set; }

        public DateTime createdDate { get; set; }
    }
}
