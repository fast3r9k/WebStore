using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDb : DbContext
    {

        public  DbSet<Product> Products { get; set; }

        public  DbSet<Brand> Brands { get; set; }

        public  DbSet<Section> Sections { get; set; }

        public WebStoreDb(DbContextOptions options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder model) => base.OnModelCreating(model);
    }
}
