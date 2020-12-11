using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.Controllers;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FromTestController : TestController
    {
        public FromTestController() : base ()
        {
            today = "FromTestController set today";
        }
    }
}
