using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticalExercise_RO.Data.Utility;
using PracticalExercise_RO.Web.Utility;
using Microsoft.AspNetCore.Mvc.Formatters;
using PracticalExercise_RO.Library.Utility;
using PracticalExercise_RO.Data.Repositories;
using PracticalExercise_RO.Library.Services;

namespace PracticalExercise_RO.Web
    {
    public class Startup
        {
        public Startup(IConfiguration configuration)
            {
            Configuration = configuration;
            }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            {

            var dbConnection = Configuration.GetValue<String>("DBConnection");
            //add middlewares 
            var builder = services.AddMvc();
            builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter()); o.RespectBrowserAcceptHeader = true; o.OutputFormatters.Add(new XmlSerializerOutputFormatter()); });
            builder.AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //add authentication
            
            services.AddResponseCompression();

            //add dependecy injections
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPractitionerRepository, PractitionerRepository>();
            services.AddScoped<IPractitionerService, PractitionerService>();
            services.AddTransient<ILoadData, LoadData>();
            services.AddTransient<ICacheService, CacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
            if (env.EnvironmentName.Equals("Developer") || env.EnvironmentName.Equals("Development"))
                {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                }
            else
                {
                app.UseExceptionHandler("/Home/Error");
                }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                    {
                    context.Request.Path = "/Home/PageNotFound";
                    await next();
                    }
                else if (context.Response.StatusCode == 403)
                    {
                    context.Request.Path = "/Home/ForbiddenError";
                    await next();
                    }
            });

            app.UseStaticFiles();

            app.UseValidateUserMiddleware();

            app.UseResponseCompression();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "CheckSite",
                    template: "checksite.aspx",
                    defaults: new { controller = "Home", action = "CheckSite" });
            });
            }
        }
    }
