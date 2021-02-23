using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Common
{
    public interface IMyService
    {
        public string StartProjectKey();
        public int CacheHours();
        public string GetWhoCallMethod();
    }
    public class MyService : IMyService
    {
        private IConfiguration _config;

        public MyService(IConfiguration config)
        {
            _config = config;
        }

        public string StartProjectKey()
        {
            return _config["StartProjectKey"];
        }

        public int CacheHours()
        {
            return _config["Cache_Hours"] == null ? 0 : Convert.ToInt32(_config["Cache_Hours"]);
        }
        public string GetWhoCallMethod()
        {
            var callMethodParent= new StackTrace().GetFrame(2).GetMethod();
            string callMethodParenController = callMethodParent.ReflectedType.Name;
            string callMethodParenName = callMethodParent.Name;

            string callName = new StackTrace().GetFrame(1).GetMethod().Name;
            //MethodBase.GetCurrentMethod().Name

            return $"{callMethodParenController}_{callMethodParenName}_{callName}";
        }


    }
}
