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
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult List()
        {
            var products = context.Products.ToList();

            return View(products);
        }

        //add method allowing user to add products to the database
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Product = context.Products.OrderBy(p => p.Name).ToList();
            return View("Edit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Product = context.Products.OrderBy(p => p.ProductID).ToList();
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                    TempData["message"] = $"{product.Name} was added to the database.";
                }
                else
                {
                    context.Products.Update(product);
                    TempData["message"] = $"{product.Name} was updated.";
                }
                context.SaveChanges();
                return RedirectToAction("List", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Decks = context.Products.OrderBy(p => p.ProductID).ToList();
                return View(product);
            }
        }
        //delete methods allowing user to delete products 
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = $"{product.Name} was deleted from database.";
            return RedirectToAction("List", "Product");
        }
    }
}
