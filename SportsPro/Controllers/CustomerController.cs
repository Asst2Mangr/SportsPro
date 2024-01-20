using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SportsProContext _context;

        public CustomerController(SportsProContext context)
        {
            _context = context;
        }

        public IActionResult CustomerList()
        {
            var customers = _context.Customers.ToList();
            return View(customers);

        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var countries = _context.Countries.ToList();
            ViewBag.Country = countries;

            return View("CustomerEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Save";
            var countries = _context.Countries.ToList();
            ViewBag.Countries = countries;
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("CustomerEdit", customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    _context.Customers.Add(customer);
                }
                else
                {
                    _context.Customers.Update(customer);
                }
                _context.SaveChanges();
                return RedirectToAction("CustomerList");
            }
            return View("CustomerEdit", customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("CustomerDelete", customer);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(Customer model)
        {
            var customer = _context.Customers.Find(model.CustomerID);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("CustomerList");
        }
    }
}
