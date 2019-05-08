using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Entities
{
    public class MessageLog : iFogBugz
    {
        [Key]
        public int LogId { get; set; }

        [MaxLength(2000)]
        public string LogMessage { get; set; }

        public DateTime createdDate { get; set; }
    }
}
