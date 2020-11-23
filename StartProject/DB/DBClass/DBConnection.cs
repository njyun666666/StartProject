using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.DB.DBClass
{
    public class DBConnection : DB_Base
    {
        public static IConfiguration Configuration { get; set; }

        public void Connection(string dbName)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("DBSettings.json");
            Configuration = builder.Build();
            String ip = Configuration[dbName + ":ip"];
            String user = Configuration[dbName + ":user"];
            String pwd = Configuration[dbName + ":pwd"];
            Connection(ip, user, pwd);
        }
    }
}
