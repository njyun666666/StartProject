using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Models
{
    public class AccountModel
    {
        public string UID { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// cookie Expires Date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:ss:dd}")]
        public DateTime ExpiresDate { get; set; }
    }
}
