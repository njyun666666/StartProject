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
            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                // Extract credentials
                string encodedAuth = authHeader.Substring("Bearer ".Length).Trim();

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

        #region ClientIP Get
        public string ClientIP_Get()
        {
            string IP = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Request.Headers["X-Forwarded-For"]))
                {
                    IP = HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(new char[] { ',' }).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(HttpContext.Request.Headers["MS_HttpContext"]))
                {
                    IP = HttpContext.Request.Headers["MS_HttpContext"];
                }
                else
                {
                    IP = HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return IP;
        }
        #endregion



    }
}
