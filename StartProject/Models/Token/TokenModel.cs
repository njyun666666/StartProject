using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Models.Token
{
    public class TokenModel
    {
        public string UID { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// cookie Expires Date
        /// </summary>
        public int ExpiresDate { get; set; }
    }
}
