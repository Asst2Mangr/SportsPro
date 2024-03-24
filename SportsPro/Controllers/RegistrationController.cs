using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SportsProContext ctx;

        public RegistrationController(SportsProContext context)
        {
            ctx = context;
        }

        public IActionResult GetCustomer()
        {
            var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
            ViewBag.Customers = new SelectList(customers, "CustomerID", "FullName");

            if (Request.Method == "POST" && Request.Form["CustomerID"].Count == 0)
            {
                ViewBag.ErrorMessage = "Please select a customer.";
            }

            return View();
        }

        [HttpPost]
        public IActionResult GetCustomer(int customerId)
        {
            if (customerId <= 0)
            {
                ViewBag.ErrorMessage = "Please select a customer.";
                var customers = ctx.Customers.ToList().OrderBy(c => c.FullName).ToList();
                ViewBag.Customers = new SelectList(customers, "CustomerID", "FullName");
                return View();
            }
            else
            {
                return RedirectToAction("Registrations", new { customerId });
            }
        }

        public IActionResult Registrations(int customerId)
        {
            var customer = ctx.Customers.FirstOrDefault(c => c.CustomerID == customerId);
            if (customer != null)
            {
                ViewBag.CustomerName = $"{customer.FirstName} {customer.LastName}";
            }
            return View();
        }

    }
}
