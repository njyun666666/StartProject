using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StartProject.Models.Test;
using StartProject.Services.IServices;
using StartProject.ViewModels;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DBDemoController : ControllerBase
    {
        private readonly ICacheService _cache;
        private readonly IDBDemoService _dbDemoService;

        public DBDemoController(ICacheService cache, IDBDemoService dbDemoService) 
        {
            _cache = cache;
            _dbDemoService = dbDemoService;
        }

        public ActionResult Query(int? id)
        {
            var a = this.ControllerContext.RouteData;
            return Ok(new OKResponse() { Data = _dbDemoService.Table_1_DB_Query(id) });
        }
        public ActionResult Query_Output()
        {
            return Ok(new OKResponse() { Data = _dbDemoService.Table_1_DB_Query_Output() });
        }
        public ActionResult Query_Output_id(int id)
        {
            return Ok(new OKResponse() { Data = _dbDemoService.Table_1_DB_QueryFirstOrDefault(id) });
        }

        public ActionResult Execute_Output()
        {
            return Ok(new OKResponse() { Data = _dbDemoService.Table_1_Execute_Output() });
        }


    }
}
