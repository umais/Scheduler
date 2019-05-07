using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SchedulerLibrary.Entities;
using System.Linq;
namespace SchedulerLibrary.EntityFramework.DbContexts
{
    public class JobDbContext:DbContext
    {

        public JobDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           var config = new ConfigurationBuilder()
          .AddJsonFile("appConfig.json", true, true)
          .Build();
          var constr= config["tickets_database"].ToString();
            optionsBuilder.UseSqlServer(constr);
        }

        public DbSet<FogBugzTickets> Tickets { get; set; }


     
     
    }


}
