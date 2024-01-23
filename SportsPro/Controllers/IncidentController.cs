﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var incident = ctx.Incidents.Find(id);

            if (incident == null)
            {
                return NotFound();
            }

            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            var products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            var technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();

            ViewBag.Action = "Edit";
            ViewBag.Customers = customers;
            ViewBag.Products = products;
            ViewBag.Technicians = technicians;

            return View("IncidentEdit", incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                ctx.Incidents.Update(incident);
                ctx.SaveChanges();
                return RedirectToAction("IncidentList");
            }

            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            var products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            var technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();

            ViewBag.Action = "Edit";
            ViewBag.Customers = customers;
            ViewBag.Products = products;
            ViewBag.Technicians = technicians;

            return View("IncidentEdit", incident);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var incident = ctx.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Incident incident)
        {
            ctx.Incidents.Remove(incident);
            ctx.SaveChanges();
            return RedirectToAction("IncidentList", "Incident");
        }

    }
}