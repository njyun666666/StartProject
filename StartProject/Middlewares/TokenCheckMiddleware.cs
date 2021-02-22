using Microsoft.AspNetCore.Http;
using StartProject.Enums;
using StartProject.Models.Token;
using StartProject.Services.IServices;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartProject.Middlewares
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ICacheService _cache;
        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITokenService tokenService)
        {
            httpContext.Response.ContentType = "application/json;charset=utf-8";
            string token = httpContext.Request.Headers["token"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
            {
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new NoTokenResponse()));
            }
            else
            {
                try
                {
                    TokenModel tokenModel;

                    try
                    {
                        tokenModel = tokenService.TokenDecrypt(token);
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogInformation(ex.Message);
                        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new DecryptErrorResponse()));
                        return;
                    }

                    if (tokenModel == null)
                    {
                        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new DecryptErrorResponse()));
                        return;
                    }

                    ResponseCodeEnum tokenCheck = tokenService.TokenCheck();

                    if (tokenCheck != ResponseCodeEnum.success)
                    {
                        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ResponseViewModel(tokenCheck)));
                        return;
                    }



                }
                catch (Exception ex)
                {
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new SystemContentErrorResponse(ex.ToString())));
                    return;
                }

                await _next(httpContext);
            }




        }

        private void AddHeader(HttpContext httpContext, string key, string value)
        {
            if (httpContext.Request.Headers.ContainsKey(key))
            {
                httpContext.Request.Headers.Remove(key);
            }
            httpContext.Request.Headers.Add(key, value);
        }



    }
}
