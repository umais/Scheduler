using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerLibrary.Entities
{
    public class FogBugzUsers
    {
        public int PersonId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsNotified { get; set; }

        public string lastActivity { get; set; }
    }
}
