using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartProject.Middlewares
{
	public static class ExceptionMiddleware
	{
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse()));
                });
            });
        }
    }
}
