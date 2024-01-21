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
    }
}
