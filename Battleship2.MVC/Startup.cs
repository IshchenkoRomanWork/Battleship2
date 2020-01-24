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


            //services.AddDbContext<BattleshipIdentityContext>(options => options.UseInMemoryDatabase(databaseName: "battleShipIdentityDb"));
            //services.AddDbContext<BattleShipContext>(options => options.UseInMemoryDatabase(databaseName: "battleShipDb"));

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<Helper>();


            services.AddSingleton<ActiveGames>();

            services.AddSignalR();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews(options => 
            {
                options.Filters.Add(typeof(ExceptionFilter));
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
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //var oprions = RewriteOptions()
            //.AddRedirect("(.*)/api(.*)", Configuration)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BattleShipHub>("/gamehub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "ApiRoutes",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
            //Proxy
            //app.Use((context, next) =>
            //{
            //    if (context.Request.Path.StartsWithSegments("/api"))
            //    {
            //        context.Request.Host = new HostString(Configuration["apiLocation"]);
            //        context.Request.
            //    }
            //    return next();
            //});

        }
    }
}
