using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Throw(string id) => throw new ApplicationException(id);
        public IActionResult SecondAction() => Content("Second action");
        public IActionResult Blogs() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Shop() => View();
        public IActionResult Error404() => View();
    }
}
