﻿using Microsoft.AspNetCore.Mvc;
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
            var products = context.Products.ToList();

            return View(products);
        }

        //add method allowing user to add deck to the database
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
                    context.Products.Add(product);
                else
                    context.Products.Update(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Decks = context.Products.OrderBy(p => p.ProductID).ToList();
                return View(product);
            }
        }
        //delete methods allowing user to delete decks 
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
