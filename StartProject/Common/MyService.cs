using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Common
{
    public interface IMyService
    {
        public string StartProjectKey();
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


    }
}
