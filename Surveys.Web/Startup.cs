using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Localization;

namespace Surveys.Web
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            ConfigurationBuilder cb = new ConfigurationBuilder();
            cb.SetBasePath(env.ContentRootPath);
            cb.AddJsonFile("appsettings.json");
            cb.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            Configuration = cb.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(o =>
            {
                o.DefaultRequestCulture = new RequestCulture("cs-cz");
                o.RequestCultureProviders.Clear();
            });
            services.AddTransient<Surveys.SurveyService>();
            services.AddDbContext<DA.SurveysDataContext>(o => o.UseSqlServer(Configuration["ConnectionString"]));
            services.AddOptions();
            services.Configure<WebSettings>(Configuration.GetSection("Settings"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRequestLocalization();

            app.UseStaticFiles();

            app.UseMiddleware<ClientIdMiddleware>();

            app.UseMvc(r =>
            {
                r.MapRoute("Survey", "survey/{id}", new { controller = "Survey", action = "Index" });
                r.MapRoute("SurveyReport", "survey/{id}/report", new { controller = "Survey", action = "Report" });
                r.MapRoute("SurveySent", "survey/{id}/sent", new { controller = "Survey", action = "SendSurveyResult" });
                r.MapRoute("Default", "", new { controller = "Home", action = "Index" });
                r.MapRoute("DefaultUniversal", "{controller}/{action}");
            });
        }
    }
}
