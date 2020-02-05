using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.MVC.Models;
using Battleship2.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Battleship2.Core.Interfaces;
using BattleShip2.BusinessLogic.Services;
using Battleship2.MVC.Filters;
using BattleShip2.BusinessLogic.Intefaces;
using Battleship2.MVC.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using React.AspNet;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.ChakraCore;
using React;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Battleship2.MVC
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
            string BattleShipConnectionString = Configuration.GetConnectionString("BattleShipConnection");
            string IdentityConnectionString = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<BattleShipIdentityContext>(options => options.UseSqlServer(IdentityConnectionString));
            services.AddDbContext<BattleShipContext>(options => options.UseSqlServer(BattleShipConnectionString));

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            //loggerFactory.MinimumLevel = LogLevel.Debug;
            //loggerFactory.AddDebug(LogLevel.Debug);
            //var logger = loggerFactory.CreateLogger("Startup");
            //logger.LogWarning("Logger configured!");


            //services.AddDbContext<BattleshipIdentityContext>(options => options.UseInMemoryDatabase(databaseName: "battleShipIdentityDb"));
            //services.AddDbContext<BattleShipContext>(options => options.UseInMemoryDatabase(databaseName: "battleShipDb"));

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<Helper>();


            services.AddSingleton<ActiveGames>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            // Make sure a JS engine is registered, or you will get an error!
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
              .AddChakraCore();

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews(options => 
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                app.UseSpaStaticFiles();
            }

            //JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            //JsEngineSwitcher.Current.EngineFactories.AddV8();
            app.UseHttpsRedirection();
            app.UseReact(config => {
                //If you want to use server-side rendering of React components,
                //add all the necessary JavaScript files here. This includes
                //your components as well as all of their dependencies.
                //See http://reactjs.net/ for more information. Example:

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //  .SetLoadBabel(false)
                //  .AddScriptWithoutTransform("~/js/bundle.server.js");

                config
                    //.SetLoadBabel(true)
                    //.SetLoadReact(true)
                    //.AddScript("~/js/dist/reactgamehub.jsx")
                    //.AddScript("~/js/dist/testReactSignalR.jsx")
                    .SetUseDebugReact(true);
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BattleShipHub>("/gamehub");
                endpoints.MapHub<TestHub>("/testhub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "ApiRoutes",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
