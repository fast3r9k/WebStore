using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestApi;

namespace WebStore.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValuesService _ValueService;
        public WebApiController(IValuesService ValueService) => _ValueService = ValueService;
        public IActionResult Index()
        {
            var values = _ValueService.Get();
            return View(values);
        }
    }
}
