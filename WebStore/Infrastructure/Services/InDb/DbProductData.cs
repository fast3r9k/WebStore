using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InDb
{
    public class DbProductData : IProductData
    {
        private readonly WebStoreDb _db;

        public DbProductData(WebStoreDb db) => _db = db;
        public IEnumerable<Section> GetSections() => _db.Sections;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;
            if (Filter?.BranId != null)
                query = query.Where(product => product.BrandId == Filter.BranId);
            if (Filter?.SeconId != null)
                query = query.Where(product => product.SectionId == Filter.SeconId);
            return query;
        }
    }
}
