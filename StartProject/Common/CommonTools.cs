using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Common
{
    public class CommonTools
    {
        public static int GetTimeStamp()
        {
            return GetTimeStamp(DateTime.UtcNow.ToLocalTime());
        }

        public static string GetFormatTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int GetTimeStamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return ((int)span.TotalSeconds);
        }

    }
}
