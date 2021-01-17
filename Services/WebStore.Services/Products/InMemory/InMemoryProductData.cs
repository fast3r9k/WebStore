using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Products.InMemory
{
    [Obsolete ("Use DbProductData.cs", true)]
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public Section GetSectionById(int id) { throw new NotSupportedException(); }

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public Brand GetBrandById(int id) { throw new NotSupportedException(); }

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
                var query = TestData.Products;
                if (Filter?.SeconId != null)
                    query = query.Where(product => product.SectionId == Filter.SeconId);
                if (Filter?.BranId != null)
                    query = query.Where(product => product.BrandId == Filter.BranId);
                return query;
            }

        public Product GetProductById(int id) { throw new NotSupportedException(); }
        public void Update<T>(T Entity) where T : class { throw new NotImplementedException(); }
    }
}
