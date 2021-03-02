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
using StartProject.ViewModels;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICacheService _cache;
        private readonly IAccountService _accountService;
        private readonly IMyService _myService;

        public AccountController(ICacheService cache, IAccountService accountService, IMyService myService)
        {
            _cache = cache;
            _accountService = accountService;
            _myService = myService;
        }

        public ActionResult<List<WeatherForecast>> Index()
        {

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

        public IActionResult ip([FromHeader] string ip)
        {
            return Ok(new OKResponse() { Data = ip });
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
