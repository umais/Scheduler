using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Entities;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Repositories.Abstract
{
    interface IRepository
    {
       int Insert(iFogBugz item);

        List<FogBugzTickets> getTickets();

        int Update(iFogBugz item);

    }
}
