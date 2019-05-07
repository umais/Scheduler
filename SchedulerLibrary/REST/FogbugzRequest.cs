using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchedulerLibrary.Entities;
using Microsoft.Extensions.Configuration;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.REST
{
    public class FogbugzRequest
    {
        private static FogbugzRequest Instance;
        public string FogBugzAPIURL { get; set; }
        public string Token { get; set; }
        public enum Operation
        {
            Logon=0,
            New=1,
            ListFogBugzUsers=2,
            ListProjects=3
        }

        IConfiguration config;

        public string[] Operations { get; set; }

        public RestClient Client { get; set; }

        private FogbugzRequest()
        {
            this.Client = new RestClient();
            config = new ConfigurationBuilder()
    .AddJsonFile("appConfig.json", true, true)
    .Build();
            this.Client.BaseUrl = new Uri(config["fogbugz_url"]);
      

       
        }

        public static FogbugzRequest GetInstance()
        {
            if(Instance==null)
            {
                Instance = new FogbugzRequest();
                Instance.Token = Instance.FogBugzLogin(Instance.config["fb_username"], Instance.config["fb_pwd"]);
            }
            return Instance;
        }

        private  string FogBugzLogin(string username,string password)
        {
            var request = new RestRequest("logon", Method.POST);
            request.AddHeader("Content-Type", "application/json"); 
            request.AddJsonBody(new { email = username, password = password });
            var response = Client.Execute(request);
            JObject res= JsonConvert.DeserializeObject<JObject>(response.Content);
            var token = (string)res.SelectToken("data.token");
            return token;

        }

        public List<iFogBugz> getFogBugzItems(FogBugzPostObject o,int counter=0) 
        {
            int errors = 0;
            int callCounter = counter;
            List<iFogBugz> fbUsers = new List<iFogBugz>();

          
            try
            {

            
            var response = MakeRequest(o);
       
                errors = CheckErrors(response);
                if (errors == 0)
                {
                    fbUsers = ParseResponse(response,(Operation) o.Operation);
                }
                else
                {
                    if (callCounter < 3)
                    {
                        ++callCounter;
                        Instance.Token = Instance.FogBugzLogin(Instance.config["fb_username"], Instance.config["fb_pwd"]);
                        fbUsers = getFogBugzItems(o,callCounter);
                    }
                    else
                        throw new Exception(string.Format("Some Error Occured Making the {0} Request.",o.Command));
                        //Write Code to Log this Error.

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return fbUsers;
        }

       


        public int CreateNewCase(FogBugzTickets t)
        {
            int ticketId = 0;
          var response=  MakeRequest(new FogBugzPostObject { Operation = 1, payload = new  {token=this.Token,sTitle=t.TicketTitle,sEvent=t.TicketContent,sProject=t.TicketProject,ixPersonAssignedTo=t.PersonAssignedTo,ixFixFor=t.MileStone } });

            JObject res = JsonConvert.DeserializeObject<JObject>(response.Content);
            ticketId=(int)res.SelectToken("data.case.ixBug");
        
            return ticketId;
        }

        private int CheckErrors(IRestResponse response)
        {
            int errorCount = 0;
          
            JObject res = JsonConvert.DeserializeObject<JObject>(response.Content);
            var errors= res.SelectToken("errors");
            foreach(JObject j in errors)
            {
                errorCount++;
            }
    
            return errorCount;
        }

        private IRestResponse MakeRequest(FogBugzPostObject o)
        {
            IRestResponse res=null;
            var request = new RestRequest(o.Command, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(o.payload);
            res = Client.Execute(request);

            //On Successfull Call Make sure to Log this Request as well.
            return res;
        }

        public List<iFogBugz> ParseResponse(IRestResponse response, Operation operation)
        {

            List<iFogBugz> fbItems = new List<iFogBugz>();
            JObject res = JsonConvert.DeserializeObject<JObject>(response.Content);


            switch (operation)
            {
                case Operation.ListFogBugzUsers:

                    var peopleList = res.SelectToken("data.people");

                    foreach (JObject o in peopleList)
                    {
                        fbItems.Add(new FogBugzUsers()
                        {
                            PersonId = (int)o.SelectToken("ixPerson"),
                            FullName = (string)o.SelectToken("sFullName"),
                            Email = (string)o.SelectToken("sEmail"),
                            IsAdministrator = (bool)o.SelectToken("fAdministrator"),
                            IsNotified = (bool)o.SelectToken("fNotify")

                        });

                    }
                    break;
                case Operation.ListProjects:
                    var projectList = res.SelectToken("data.projects");
                    foreach (JObject o in projectList)
                    {
                        fbItems.Add(new FogBugzProjects()
                        {
                            ProjectId = (int)o.SelectToken("ixProject"),
                            ProjectName = (string)o.SelectToken("sProject"),
                            OwnerId = (int)o.SelectToken("ixPersonOwner"),
                            OwnerName = (string)o.SelectToken("sPersonOwner"),
                            IsDeleted = (bool)o.SelectToken("fDeleted")

                        });

                    }
                    break;
            }
            
        

            return fbItems;
        }
    }
}
//data.case.ixBug