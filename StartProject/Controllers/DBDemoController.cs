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
using TestProject.Controllers;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DBDemoController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDBDemoService _dbDemoService;

        public DBDemoController(IMemoryCache memoryCache, IDBDemoService dbDemoService) 
        {
            this._memoryCache = memoryCache;
            this._dbDemoService = dbDemoService;
        }

        public List<Table_1Model> Query(int? id)
        {
            return _dbDemoService.Table_1_DB_Query(id);
        }
        public BaseViewModel<List<Table_1Model>> Query_Output()
        {
            return _dbDemoService.Table_1_DB_Query_Output();
        }
        public Table_1Model Query_Output(int id)
        {
            return _dbDemoService.Table_1_DB_QueryFirstOrDefault(id);
        }

        public BaseViewModel Execute_Output()
        {
            return _dbDemoService.Table_1_Execute_Output();
        }


    }
}
