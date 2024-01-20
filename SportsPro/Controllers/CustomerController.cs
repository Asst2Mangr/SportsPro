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

        public IActionResult Index()
        {
            return View();
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
            ViewBag.Countries = _context.Countries.OrderBy(c => c.Name).ToList();

            return View("CustomerEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = _context.Countries.OrderBy(c => c.Name).ToList();
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
                    _context.Customers.Add(customer);
                else
                    _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("CustomerList", "Customer");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = _context.Countries.OrderBy(c => c.Name).ToList();
                return View(customer);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList", "Customer");
        }
    }
}
