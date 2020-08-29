using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StartProject.Models;
using StartProject.Services;

namespace StartProject.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        AccountModel baseAccount;

        protected BaseController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            baseAccount = null;
        }


        /// <summary>
        /// check login
        /// </summary>
        protected void CheckLogin()
        {
            string encodedAuth = string.Empty;
            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                // Extract credentials
                encodedAuth = authHeader.Substring("Bearer ".Length).Trim();

                //decode
                string decodeAuth = AuthService.TokenKeyAES_decrypt(encodedAuth);

                if(!string.IsNullOrWhiteSpace(decodeAuth))
                {
                    baseAccount = JsonSerializer.Deserialize<AccountModel>(decodeAuth);

                    if ( string.IsNullOrWhiteSpace(baseAccount.UID) || baseAccount.ExpiresDate < DateTime.Now)
                    {
                        baseAccount = null;
                    }

                }

            }
            // Handle what happens if that isn't the case
            // throw new Exception("The authorization header is either empty or isn't Basic.");
            // return new StatusCodeResult(StatusCodes.Status403Forbidden);

        }





    }
}
