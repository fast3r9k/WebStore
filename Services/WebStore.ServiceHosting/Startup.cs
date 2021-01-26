using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InDb;
using WebStore.Services.Products.InMemory;

namespace WebStore.ServiceHosting
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
            services.AddDbContext<WebStoreDb>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<WebStoreDbInitializer>();

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

            services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
            services
               .AddScoped<IProductData, DbProductData>()
               .AddScoped<ICartService, InCookiesCartService>()
               .AddScoped<IOrderService, DbOrderService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                const string webstoreApiXML = "WebStore.ServiceHosting.xml";
                const string webstoreDomainXML = "WebStore.Domain.xml";
                const string debugPath = "bin/Debug/net5.0";
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.ServiceHosting", Version = "v1" });

                c.IncludeXmlComments(webstoreApiXML);

                if (File.Exists(webstoreDomainXML))
                    c.IncludeXmlComments(webstoreDomainXML);
                else if(File.Exists(Path.Combine(debugPath,webstoreDomainXML)))
                    c.IncludeXmlComments(Path.Combine(debugPath, webstoreDomainXML));
                    
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.ServiceHosting v1"));
                
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
