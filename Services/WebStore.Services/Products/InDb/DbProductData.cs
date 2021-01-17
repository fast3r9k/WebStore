using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InDb
{
    public class DbProductData : IProductData
    {
        private readonly WebStoreDb _db;

        public DbProductData(WebStoreDb db) => _db = db;
        public IEnumerable<Section> GetSections() => _db.Sections.Include(sections => sections.Products);

        public Section GetSectionById(int id) => _db.Sections
           .Include(section => section.Products)
           .FirstOrDefault(s => s.Id == id);

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(brand => brand.Products);

        public Brand GetBrandById(int id) => _db.Brands
           .Include(brand => brand.Products)
           .FirstOrDefault(b=> b.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            
            else
            {
                if (Filter?.BranId != null)
                    query = query.Where(product => product.BrandId == Filter.BranId);

                if (Filter?.SeconId != null)
                    query = query.Where(product => product.SectionId == Filter.SeconId);
            }

            return query;
        }

        public Product GetProductById(int id) => _db.Products
           .Include(p => p.Brand)
           .Include(p => p.Section)
           .FirstOrDefault(p => p.Id == id);

        public void Update<T>(T Entity) where T : class
        {
            _db.Update(Entity);

            _db.SaveChanges();
        }
    }
}
