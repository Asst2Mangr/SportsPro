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

        public IActionResult List(string filter)
        {
            var incidents = GetIncidents(filter);

            var viewModel = new IncidentViewModel
            {
                Incidents = incidents,
                Filter = filter
            };

            return View("List", viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            var products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            var technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();

            var viewModel = new AddEditIncidentViewModel
            {
                Customers = customers,
                Products = products,
                Technicians = technicians,
                CurrentIncident = new Incident(),
                Operation = "Add"
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEditIncidentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ctx.Incidents.Add(viewModel.CurrentIncident);
                ctx.SaveChanges();
                return RedirectToAction("List");
            }

            viewModel.Customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            viewModel.Products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            viewModel.Technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();
            viewModel.Operation = "Add";

            return View("Edit", viewModel);
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

            var viewModel = new AddEditIncidentViewModel
            {
                Customers = customers,
                Products = products,
                Technicians = technicians,
                CurrentIncident = incident,
                Operation = "Edit"
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditIncidentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ctx.Incidents.Update(viewModel.CurrentIncident);
                ctx.SaveChanges();
                return RedirectToAction("List");
            }

            viewModel.Customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            viewModel.Products = ctx.Products.ToList().OrderBy(p => p.Name).ToList();
            viewModel.Technicians = ctx.Technicians.ToList().OrderBy(t => t.Name).ToList();
            viewModel.Operation = "Edit";

            return View("Edit", viewModel);
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
            return RedirectToAction("List", "Incident");
        }
        private List<Incident> GetIncidents(string filter)
        {
            var query = ctx.Incidents.Include(i => i.Customer).Include(i => i.Product).AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(i => i.Title.Contains(filter));
            }

            return query.ToList();
        }
    }
}
