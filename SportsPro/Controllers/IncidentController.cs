using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private readonly SportsProContext ctx;

        public IncidentController(SportsProContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IncidentList()
        {
            var incidents = ctx.Incidents.Include(i => i.Customer).Include(i => i.Product).ToList();
            return View(incidents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            var products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            var technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();

            ViewBag.Action = "Add";
            ViewBag.Customers = customers;
            ViewBag.Products = products;
            ViewBag.Technicians = technicians;

            return View("IncidentEdit", new Incident());
        }

        [HttpPost]
        public IActionResult Add(Incident incident)
        {
            if (ModelState.IsValid)
            {
                ctx.Incidents.Add(incident);
                ctx.SaveChanges();
                return RedirectToAction("IncidentList");
            }

            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            var products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            var technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();

            ViewBag.Action = "Add";
            ViewBag.Customers = customers;
            ViewBag.Products = products;
            ViewBag.Technicians = technicians;

            return View("IncidentEdit", incident);
        }

    }
}
