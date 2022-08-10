using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyEshop.Data;
using MyEshop.Data.Repository;

namespace MyEshop
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

            services.AddRazorPages();

            #region Db Context

            services.AddDbContext<MyEshopContext>(options =>
            { options.UseSqlServer("Data Source =.;Initial Catalog=EshopCore_DB;Integrated Security=true"); });

            #endregion

            #region IOC

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Autenticaton

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Option =>
            {
                Option.LoginPath = "/Account/Login";
                Option.LogoutPath = "/Account/Logout";
                Option.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            #endregion
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
           
            app.UseAuthorization();

            app.Use(async (Context, next) =>
            {
                if (Context.Request.Path.StartsWithSegments("/Admin"))
                {
                    if (!Context.User.Identity.IsAuthenticated)
                    {
                        Context.Response.Redirect("/Account/Login");
                    }
                    else if (!bool.Parse(Context.User.FindFirstValue("IsAdmin")))
                    {
                        Context.Response.Redirect("/Account/Login");
                    }
                }

                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
