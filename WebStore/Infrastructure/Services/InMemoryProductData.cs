using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entityes;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
                var query = TestData.Products;
                if (Filter?.SecondId != null)
                    query = query.Where(product => product.SectionId == Filter.SecondId);
                if (Filter?.BranID != null)
                    query = query.Where(product => product.BrandId == Filter.BranID);
                return query;
            }
        }
}
