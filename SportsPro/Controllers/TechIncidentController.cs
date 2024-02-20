using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {   
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ViewResult Get()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        public IActionResult List(int id)
        {
            var session = new SportsProSession(HttpContext.Session);
            if (id != 0)
            {
                var technician = context.Technicians.Find(id);
                var Model = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incidents = context.Incidents
                        .Include(i => i.Customer)
                        .Include(i => i.Product)
                        .Where(i => i.TechnicianID == technician.TechnicianID &&
                                    i.DateClosed == null).ToList()
                };
                if(Model.Incidents.Count == 0)
                {
                    TempData["message"] = $"No open incidents for this Technician";
                }
                return View(Model);
            }
            else
            {
                TempData["message"] = $"Please enter a Technician";
                return RedirectToAction("Get");
            }
        }
    }
}
