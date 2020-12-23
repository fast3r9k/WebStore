using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        Section GetSectionById(int id);

        IEnumerable<Brand> GetBrands();
        Brand GetBrandById(int id);

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
        Product GetProductById(int id);

        void Update<T>(T Entity) where T : class;
    }
}
