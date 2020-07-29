﻿using System;
using System.IO;
using Common.AppContexts;
using Common.Modules;
using Common.Modules.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NbSites.Web.Apis;
using NbSites.Web.Libs.Boots;
using NbSites.Web.Libs.Data;

namespace NbSites.Web
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<TestAppService>();

            var mvcBuilder = services.AddMvc();
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //"more then one DbContext named 'xxx' was found => move this code to startup.cs to hack it
            services.AddDbContext<LinksDbContext>(options =>
            {
                string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LinksDbContext.sqlite");
                //string connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", dbFilePath);
                string connectionString = string.Format("Data Source={0}", dbFilePath);
                options.UseSqlite(connectionString);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMyStaticFiles(Environment, _logger);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var linksDbContext = scope.ServiceProvider.GetRequiredService<LinksDbContext>();
                linksDbContext.Database.EnsureCreated();
            }

            var myAppContext = MyAppContext.Current;
            myAppContext.Items["EntryUri"] = "~/Links/LinkItem/Index";
        }
    }
}