
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDb _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WebStoreDbInitializer> _Logger;

        public WebStoreDbInitializer(WebStoreDb db,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;

            _Logger = Logger;
        }

        public void Initialize()
        {
            _Logger.LogInformation("Initialising database");
            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("There are unapplied migrations");
                db.Migrate();
                _Logger.LogInformation("Migrations complete");
            }
            else
                _Logger.LogInformation("There are no unapplied migrations");

            try
            {
                InitializeProduct();
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Initialization failed (Catalog products)");
                throw;
            }

            try
            {
                InitializeIdentityAsync().Wait();
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Initialization failed (Identity system)");
                throw;
            }
        }

        public void InitializeProduct()
        {
            if(_db.Products.Any())
                return;

            using (_db.Database.BeginTransaction())
            {
                
                _db.Sections.AddRange(TestData.Sections);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                _db.Database.CommitTransaction();
            }

            using (_db.Database.BeginTransaction())
            {

                _db.Brands.AddRange(TestData.Brands);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                _db.Database.CommitTransaction();
            }

            using (_db.Database.BeginTransaction())
            {

                _db.Products.AddRange(TestData.Products);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                _db.Database.CommitTransaction();
            }
        }

        private async Task InitializeIdentityAsync()
        {
            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                    await _RoleManager.CreateAsync(new Role {Name = RoleName});
            }

            await CheckRole(Role.Admin);
            await CheckRole(Role.User);

            if (await _UserManager.FindByNameAsync(User.Admin) is null)
            {
                var admin = new User
                {
                    UserName = User.Admin
                };
                var creationResult = await _UserManager.CreateAsync(admin, User.DefaultADminPassword);
                if (creationResult.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.Admin);
                else
                {
                    var errors = creationResult.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"An error occurred while creating the admin role {string.Join(",", errors)}");
                }
            }

        }
    }
}
