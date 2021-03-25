using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartProject.Common;
using StartProject.Enums;
using StartProject.Services.IServices;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICacheService _cache;
        private readonly IAccountService _accountService;
        private readonly IMyService _myService;

        public AuthController(ICacheService cache, IAccountService accountService, IMyService myService)
        {
            _cache = cache;
            _accountService = accountService;
            _myService = myService;
        }

        public async Task<IActionResult> Check([FromHeader] string Token)
        {
            //string idToken="eyJhbGciOiJSUzI1NiIsImtpZCI6IjZhOGJhNTY1MmE3MDQ0MTIxZDRmZWRhYzhmMTRkMTRjNTRlNDg5NWIiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiMTA2NTg5Mjc1ODA5MS0xMHR0c2JvOTAzMnZjMW5yNDVzYmc3aTVhcWViNDhyZi5hcHBzLmdvb2dsZXVzZXJjb250ZW50LmNvbSIsImF1ZCI6IjEwNjU4OTI3NTgwOTEtMTB0dHNibzkwMzJ2YzFucjQ1c2JnN2k1YXFlYjQ4cmYuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDQxMjMzMzI4MTM0MDQ2NDE5NTgiLCJoZCI6IjlzcGxheS5jb20iLCJlbWFpbCI6Imp5dW5faHVhbmdAOXNwbGF5LmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoidkNVcHdlcnc0MFcxWDhTQ1lzRVppUSIsIm5hbWUiOiLpu4Pkv4rnkYsiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDUuZ29vZ2xldXNlcmNvbnRlbnQuY29tLy1tN3NPMFVXSllVMC9BQUFBQUFBQUFBSS9BQUFBQUFBQUFBQS9BTVp1dWNuZXp3MXdyS0VLR3NydWhaNmxxZmRnNFlHR19BL3M5Ni1jL3Bob3RvLmpwZyIsImdpdmVuX25hbWUiOiLkv4rnkYsiLCJmYW1pbHlfbmFtZSI6Ium7gyIsImxvY2FsZSI6InpoLVRXIiwiaWF0IjoxNjE2NDY2NDM3LCJleHAiOjE2MTY0NzAwMzcsImp0aSI6ImUyYjk3Y2Y4OWRjMWM2OTJkNjViZjg4MWE0YzIyMzA2MTM2OWRiZGIifQ.HDCvhXfki4qk-FtKlhXpd0zRSqLtHn91UJtJYZQbK93VRMLkD9SCsXHERmsK8Gv7yQPsyhLwTS_r48xXhpEUPa8OSFlyIhqsSYAOYUqZJvbmuCr5aA0EeGjBoJMAY7ZzgafbWvpFRow0v3k6VcI5KGXDTSCjD09GSLm_5MzrpyB7gyI6GNeyTIcaLjF9AgcCP7v2f7_wFQYnSl38Ek5Jmwqmz8wDayCcrrhJqKNDnbNXZ7QEWAI-rRtr7sxD-8Qog6HL0TSFWWA3uaQsNvAe3VHo1qLcmnXhwOQ7aZTbtqKI09zLHQUOpRgjZeQPIF5uGRMVi_vljyurXOibT5fOgA";
            ResponseCodeEnum result = ResponseCodeEnum.success;

            try
            {
                var payload = await ValidateGoogleToken(Token);
            }
            catch 
            {
                result = ResponseCodeEnum.token_not_exist;
            }
            

            return Ok(new ResponseViewModel(result,""));
        }



        private static async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string googleTokenId)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>() { "1065892758091-10ttsbo9032vc1nr45sbg7i5aqeb48rf.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleTokenId, settings);
            return payload;
        }



        public IActionResult CheckFalse([FromHeader] string Token)
        {

            ResponseCodeEnum result = ResponseCodeEnum.token_not_exist;
            


            return Ok(new ResponseViewModel(result, ""));
        }





    }

}
