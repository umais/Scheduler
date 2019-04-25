using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchedulerLibrary.Entities;
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

        public FogbugzRequest()
        {
            this.Client = new RestClient();
            this.Client.BaseUrl = new Uri("https://aligncare.fogbugz.com/api/");
        }

        public string FogBugzLogin(string username,string password)
        {
            var request = new RestRequest("logon", Method.POST);
            request.AddHeader("Content-Type", "application/json"); 
            request.AddJsonBody(new { email = username, password = password });
            var response = Client.Execute(request);
            JObject res= JsonConvert.DeserializeObject<JObject>(response.Content);
            var token = (string)res.SelectToken("data.token");
            return token;

        }

        public List<FogBugzUsers> getAllManuscriptUsers(string token)
        {
            List<FogBugzUsers> fbUsers = new List<FogBugzUsers>();
            var request = new RestRequest("listPeople", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { token, });
            var response = Client.Execute(request);
            JObject res = JsonConvert.DeserializeObject<JObject>(response.Content);

            var peopleList = res.SelectToken("data.people");
            
            foreach(JObject o in peopleList)
            {
                fbUsers.Add(new FogBugzUsers() {
                                                   PersonId = (int)o.SelectToken("ixPerson"),
                                                   FullName=(string) o.SelectToken("sFullName"),
                                                   Email=(string)o.SelectToken("sEmail"),
                                                   IsAdministrator= (bool)o.SelectToken("fAdministrator"),
                                                   IsNotified = (bool)o.SelectToken("fNotify")

                });
              
            }
            return fbUsers;
        }

        public List<FogBugzProjects> getAllProjects(string token)
        {
            List<FogBugzProjects> fbProjects = new List<FogBugzProjects>();
            var request = new RestRequest("listProjects", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { token, });
            var response = Client.Execute(request);
            JObject res = JsonConvert.DeserializeObject<JObject>(response.Content);

            var projectList = res.SelectToken("data.projects");

            foreach (JObject o in projectList)
            {
                fbProjects.Add(new FogBugzProjects()
                {
                    ProjectId = (int)o.SelectToken("ixProject"),
                    ProjectName = (string)o.SelectToken("sProject"),
                    OwnerId = (int)o.SelectToken("ixPersonOwner"),
                    OwnerName= (string)o.SelectToken("sPersonOwner"),
                    IsDeleted= (bool)o.SelectToken("fDeleted")

                });

            }
            return fbProjects;
        }


        public int CreateNewCase(Tickets t)
        {
            int ticketId = 0;

            return ticketId;
        }
    }
}
//data.case.ixBug