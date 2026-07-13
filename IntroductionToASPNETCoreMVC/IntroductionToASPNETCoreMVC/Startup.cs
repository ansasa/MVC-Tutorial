using IntroductionToASPNETCoreMVC.MiddleWares;
using IntroductionToASPNETCoreMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroductionToASPNETCoreMVC
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
            services.AddControllersWithViews();

            services.Configure<EmployeeLocationOptions>(options => {
                options.CityName = "Atlanta";
                options.CountryName = "USA";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<EmployeeLocationOptions> locationOptions)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //1
            app.Use(async (context, next) => {
                if (context.Request.Method == HttpMethods.Get
                && context.Request.Query["iscertified"] == "true")
                {
                    await context.Response.WriteAsync("Message from Custom Middleware \n");
                }
                await next();
            });


            //2
            app.UseMiddleware<RequestQueryStringMiddleWare>();

            //4
            app.Use(async (context, next) => {
                if (context.Request.Path == "/short")
                {
                    await context.Response.WriteAsync($"Request Short Circuited");
                }
                else
                {
                    await next();
                }
            });


            //5   
            app.Map("/branch", branch => {
                branch.UseMiddleware<RequestQueryStringMiddleWare>();

                branch.Run(async (context) =>
                {
                    await context.Response.WriteAsync($"BranchRun Middleware");
                });

                branch.Use(async (context, next) => {
                    await context.Response.WriteAsync($"Branch Middleware");
                });
               
            });

            //6
            app.Map("/branch2", branch2 => {
                branch2.Run(new RequestQueryStringMiddleWare().Invoke);
            });



            //3

            app.Use(async (context, next) => {
                await next();
                await context.Response.WriteAsync($"\nStatus Code: { context.Response.StatusCode}");
               
            });


            app.UseMiddleware<EmployeeLocationMiddleWare>();
            //8
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/Employeelocation")
            //    {
            //        EmployeeLocationOptions opts = locationOptions.Value;
            //        await context.Response
            //        .WriteAsync($"\n{opts.CityName}, {opts.CountryName}");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});




            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
