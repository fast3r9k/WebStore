using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        public IActionResult Shop(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BranId = BrandId,
                SeconId = SectionId
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                   .OrderBy(p =>p.Order).ToView()
            });
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
                RedirectToAction("Error404", "Home");

            return View(product.ToView());  
        }
    }
}
