using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductClients : BaseClient, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductClients(IConfiguration Configuration) : base(Configuration, WebApi.Products)
        {
        }

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => 
            Post(Address, Filter ?? new ProductFilter())
           .Content
           .ReadAsAsync<IEnumerable<ProductDTO>>()
           .Result;


        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");

        public void Update<T>(T Entity) where T : class => _ProductData.Update(Entity);
    }
}
