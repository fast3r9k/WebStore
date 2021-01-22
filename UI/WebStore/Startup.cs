using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Clients.Employees;
using WebStore.Clients.Values;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestApi;
using WebStore.Services.Data;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InDb;
using WebStore.Services.Products.InMemory;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration Configuration) => _Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDb>(opt => opt.UseSqlServer(_Configuration.GetConnectionString("Default")));
            services.AddTransient<WebStoreDbInitializer>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddScoped<IOrderService, DbOrderService>();

            services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<WebStoreDb>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(
                opt =>
                {
#if DEBUG
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredUniqueChars = 3;
#endif
                    opt.User.RequireUniqueEmail = false;
                    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                });

            services.ConfigureApplicationCookie(
                opt =>
                {
                    opt.Cookie.Name = "MyWebStore";
                    opt.Cookie.HttpOnly = true;
                    opt.ExpireTimeSpan = TimeSpan.FromDays(7);

                    opt.LoginPath = "/Account/Login";
                    opt.LogoutPath = "/Account/Logout";
                    opt.AccessDeniedPath = "/Account/AccessDenied";

                    opt.SlidingExpiration = true;
                });

            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, DbProductData>();
            services.AddScoped<IValuesService, ValuesClient>();

            services
               .AddControllersWithViews()
               .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
