using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchedulerLibrary.Entities
{
   public class Tickets
    {
        [Key]
       public int TicketId { get; set; }
        [MaxLength(200)]
       public string TicketTitle { get; set; }
        [MaxLength(500)]
        public string TicketContent { get; set; }
    }
}
