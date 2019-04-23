using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using SchedulerLibrary.Entities;
namespace SchedulerLibrary.EntityFramework.DbContexts
{
    public class JobDbContext:DbContext
    {

        public JobDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CommonEntities"].ConnectionString);
        }

        public DbSet<Tickets> Tickets { get; set; }
    }
}
