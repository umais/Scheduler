using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
namespace SchedulerLibrary
{
    public static class JobExtensions
    {
        public static void Start(this IJob job) { }
    }
}
