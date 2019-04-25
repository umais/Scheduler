using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
namespace SchedulerLibrary.REST
{
    public class FogbugzRequest
    {
        public string FogBugzAPIURL { get; set; }
        public enum Operation
        {
            Logon=0,
            New=1,
            ListFogBugzUsers=2,
            ListProjects=3
        }

        public string[] Operations { get; set; }

        public RestClient Client { get; set; }


    }
}
