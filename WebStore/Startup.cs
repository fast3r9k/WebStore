using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure.Services.InDb;
using WebStore.Infrastructure.Services.InMemory;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration Configuration) => _Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddTransient<IProductData, DbProductData>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<WebStoreDb>(opt => opt.UseSqlServer(_Configuration.GetConnectionString("Default")));
            services.AddTransient<WebStoreDbInitializer>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
