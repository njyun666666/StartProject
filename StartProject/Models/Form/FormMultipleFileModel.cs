using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Models.Form
{
    public class FormMultipleFileModel
    {
        public string account { get; set; }
        public string password { get; set; }
        public List<IFormFile> file { get; set; }
    }
}
