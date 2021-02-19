using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.DB.DBClass
{
    public interface IDBConnection
    {
        public string Connection(string dbName);
    }

    public class DBConnection : IDBConnection
    {
        private IConfiguration _config;
        /// <summary>
        /// IP
        /// </summary>
        protected string Ip { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        protected string Id { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        protected string Pw { get; set; }
        /// <summary>
        /// 連線字串
        /// </summary>
        protected string str_conn { get; set; }

        public DBConnection(IConfiguration config)
        {
            _config = config;
            
        }


        public string Connection(string dbName)
        {
            Ip = _config["DB:" + dbName + ":ip"];
            Id = _config["DB:" + dbName + ":user"];
            Pw = _config["DB:" + dbName + ":pwd"];
            str_conn = "Data Source=" + Ip + ";User ID=" + Id + ";Password=" + Pw + ";Initial Catalog=" + dbName + ";Persist Security Info=True;";
            return str_conn;
        }

    }
}
