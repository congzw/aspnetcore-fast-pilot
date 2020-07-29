using System;
using System.IO;
using Common.AppContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NbSites.Web.Libs.AppServices;
using NbSites.Web.Libs.Data;

namespace NbSites.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddScoped<ISeedAppService, SeedAppService>();
            services.AddDbContext<MyDbContext>(options =>
            {
                string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LinksDbContext.db");
                //string connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", dbFilePath);
                string connectionString = string.Format("Data Source={0}", dbFilePath);
                options.UseSqlite(connectionString);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var seedAppService = scope.ServiceProvider.GetRequiredService<ISeedAppService>();
                seedAppService.ResetDb(new ResetDbArgs());
            }

            var myAppContext = MyAppContext.Current;
            myAppContext.Items["EntryUri"] = "~/LinkItem/Index";
        }
    }
}
