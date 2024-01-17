using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context;

        public ProductController(SportsProContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var products = context.Product.ToList();

            return View(products);
        }
    }
}
