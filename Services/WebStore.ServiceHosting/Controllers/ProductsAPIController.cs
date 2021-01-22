using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsAPIController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsAPIController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _ProductData.GetSections();

        [HttpGet("sections/{id}")]
        public Section GetSectionById(int id) => _ProductData.GetSectionById(id);

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _ProductData.GetBrands();

        [HttpGet("brands/{id}")]
        public Brand GetBrandById(int id) => _ProductData.GetBrandById(id);

        [HttpPost]
        public IEnumerable<Product> GetProducts([FromBody ]ProductFilter Filter = null) => _ProductData.GetProducts(Filter);

        [HttpGet("{id}")]
        public Product GetProductById(int id) => _ProductData.GetProductById(id);

        [HttpPut]
        public void Update<T>(T Entity) where T : class => _ProductData.Update(Entity);
    }
}
