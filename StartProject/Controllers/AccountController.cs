﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StartProject.Helper;
using StartProject.Models;
using StartProject.Services.IServices;
using System.Text.Json;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAccountService _accountService;

        public AccountController(IMemoryCache memoryCache, IAccountService accountService) : base(memoryCache)
        {
            this._memoryCache = memoryCache;
            this._accountService = accountService;
        }

        public ActionResult<List<WeatherForecast>> Index()
        {
            AccountModel account = new AccountModel() 
            {
                UID="123456789",
                Account="administrator",
                UserName="admin"
            };


            List<WeatherForecast> result = ApiHelper.Get<List<WeatherForecast>, AccountModel>("https://localhost:44330/WeatherForecast", account);

            
            return result;
        }



    }
}
