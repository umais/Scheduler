using System;
using System.Collections.Generic;
using System.Text;
using SchedulerLibrary.Interfaces;
namespace SchedulerLibrary.Repositories.Concrete
{
    public class FileLogger:Ilogger
    {

        public bool Log(String value)
        {
            throw new NotImplementedException();
        }

        public bool Log(String key,String value)
        {
            throw new NotImplementedException();
        }

        public String getLogValueByKey(String key)
        {
            return "Working on it";
        }
    }
}
