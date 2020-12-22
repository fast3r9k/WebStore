using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Admin)] 
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly WebStoreDb _db;

        public CatalogController(IProductData ProductData, WebStoreDb db)
        {
            _ProductData = ProductData;
            _db = db;

        } 
        public IActionResult Index()
        {

            return View(_ProductData.GetProducts());
        }

        #region Edit
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Edit(Product product)
        {
           if(product is null) throw new ArgumentNullException(nameof(product));
           if (ModelState.IsValid)
           {


               _db.Update(product);

               _db.SaveChanges();

               return RedirectToAction("Index");
           }

           return View(product);
        }
#endregion
    }
}
