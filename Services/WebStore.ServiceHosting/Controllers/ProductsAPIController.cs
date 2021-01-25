using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsAPIController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsAPIController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _ProductData.GetSections();

        [HttpGet("sections/{id}")]
        public SectionDTO GetSectionById(int id) => _ProductData.GetSectionById(id);

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _ProductData.GetBrands();

        [HttpGet("brands/{id}")]
        public BrandDTO GetBrandById(int id) => _ProductData.GetBrandById(id);

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody ]ProductFilter Filter = null) => _ProductData.GetProducts(Filter);

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);

        [HttpPut]
        public void Update<T>(T Entity) where T : class => _ProductData.Update(Entity);
    }
}
