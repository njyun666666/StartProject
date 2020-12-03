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
    public class Start2Controller : BaseController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IStart2Service _start2Service;

        public Start2Controller(IMemoryCache memoryCache, IStart2Service start2Service) : base(memoryCache)
        {
            this._memoryCache = memoryCache;
            this._start2Service = start2Service;
        }

        public List<Table_1Model> Table_1Get(int? id)
        {
            return _start2Service.Table_1_Get(id);
        }
        public BaseViewModel Select_Output()
        {
            return _start2Service.Table_1_ref();
        }
        public BaseViewModel Output()
        {
            return _start2Service.Table_1_Para_Output();
        }


    }
}
