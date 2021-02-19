using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StartProject.Common;
using StartProject.Helper;
using StartProject.Models;
using StartProject.Services.IServices;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAccountService _accountService;
        private readonly IMyService _myService;

        public AccountController(IMemoryCache memoryCache, IAccountService accountService, IMyService myService) : base(memoryCache, myService)
        {
            _memoryCache = memoryCache;
            _accountService = accountService;
            _myService = myService;
        }

        public ActionResult<List<WeatherForecast>> Index()
        {
            ClientIP_Get();

            AccountModel account = new AccountModel()
            {
                UID = "123456789",
                Account = "administrator",
                UserName = "admin",
                ExpiresDate = DateTime.Now
            };


            List<WeatherForecast> result = ApiHelper.Get<List<WeatherForecast>, AccountModel>("https://localhost:44330/WeatherForecast", account);



            return result;
        }

        public string ip()
        {

            string a = "";
            a+= "X-Forwarded-For:" + HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(new char[] { ',' }).FirstOrDefault();
            a += "\nMS_HttpContext:" + HttpContext.Request.Headers["MS_HttpContext"];
            a += "\nRemoteIpAddress:" + HttpContext.Connection.RemoteIpAddress.ToString();
            a += "\n" + ClientIP_Get();
            return a;
        }


        public string aesen(string id)
        {
            string a = EncryptHelper.AES_encrypt(id, _myService.StartProjectKey());
            a += "\n" + EncryptHelper.AES_decrypt(a, _myService.StartProjectKey());

            return a;
        }

        public string HMACSHA256(string id, string key)
        {
            string a = EncryptHelper.ComputeHMACSHA256(id, key);

            return a;
        }

    }
}
