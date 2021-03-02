using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StartProject.Common;
using StartProject.DB;
using StartProject.DB.DBClass;
using StartProject.Filters;
using StartProject.Middlewares;
using StartProject.Services;
using StartProject.Services.IServices;

namespace StartProject
{
    public class Startup
    {
        readonly string CORSPolicy = "_CORSPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicy,
                builder =>
                {
                    builder//.WithOrigins(Configuration.GetValue<string>("App:CorsOrigins").Replace(" ", "").Split(",", StringSplitOptions.RemoveEmptyEntries))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });



            //services.AddControllers(options =>
            //{
            //    options.Filters.Add(typeof(ExceptionAttribute));
            //});



            services.AddMemoryCache();

            services.AddControllers();

            
            services.AddSingleton<IDBConnection, DBConnection>();
            services.AddSingleton<ITestDB, TestDB>();

            
            services.AddSingleton<IMyService, MyService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IDBDemoService, DBDemoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            string hostIp = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            //Console.WriteLine($"{Dns.GetHostName()}");
            //Console.WriteLine($"{env.ApplicationName}");
            int ServerIpId = 0;
            foreach (var ip in host.AddressList)
            {
                Console.WriteLine($"{ip.ToString()}");

                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    hostIp = ip.ToString();
                }
            }


            //app.UseMiddleware<FirstMiddleware>();


            //app.Use(async (context, next) =>
            //{
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});


            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Login")
                && !context.Request.Path.StartsWithSegments("/Test")
                && !context.Request.Path.StartsWithSegments("/DBConn"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<TokenCheckMiddleware>();
                });

            //app.Run(async context =>
            //{
            //    //context.Response.WriteAsync($"{message}");
            //});

            //appLifetime.ApplicationStarted.Register(() =>
            //{
            //    using (var scope = app.ApplicationServices.CreateScope())
            //    {
            //        var logService = scope.ServiceProvider.GetService<ILogService>();
            //        logService.Add(1, "");
            //        //ServerIpId = logService.LogServerStart(Dns.GetHostName(), env.ApplicationName, hostIp);
            //        Console.WriteLine($"ServerIpId : {ServerIpId}");

            //    }
            //});








            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // error redirect
            app.UseStatusCodePagesWithRedirects("/api/Account/ip");


            app.UseCors(CORSPolicy);



            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Login")
                && !context.Request.Path.StartsWithSegments("/Test")
                && !context.Request.Path.StartsWithSegments("/DBConn"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<TokenCheckMiddleware>();
                    var logService=appBuilder.ApplicationServices.GetService<ILogService>();
                    logService.Add(1,"");
                });





            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            



        }
    }
}
