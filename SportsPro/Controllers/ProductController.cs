using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            List<Product> products;
            products = new List<Product>();

            return View(products);
        }
    }
}
