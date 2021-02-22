using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StartProject.Common;
using StartProject.DB;
using StartProject.DB.DBClass;
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
                    builder.WithOrigins(Configuration.GetValue<string>("App:CorsOrigins").Replace(" ", "").Split(",", StringSplitOptions.RemoveEmptyEntries))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });



            services.AddMemoryCache();

            services.AddControllers();

            
            services.AddSingleton<IDBConnection, DBConnection>();
            services.AddSingleton<ITestDB, TestDB>();

            
            services.AddSingleton<IMyService, MyService>();

            
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IDBDemoService, DBDemoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // error redirect
            app.UseStatusCodePagesWithRedirects("/api/Account/ip");


            app.UseCors(CORSPolicy);



            //app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Login")
            //    && !context.Request.Path.StartsWithSegments("/Test")
            //    && !context.Request.Path.StartsWithSegments("/DBConn"),
            //    appBuilder =>
            //    {
            //        appBuilder.UseMiddleware<TokenCheckMiddleware>();
            //    });





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
