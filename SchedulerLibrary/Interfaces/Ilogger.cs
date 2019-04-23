using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerLibrary.Interfaces
{
    public interface Ilogger
    {
        bool Log(String key,String value);
        bool Log(String value);

        String getLogValueByKey(String key);
    }
}
