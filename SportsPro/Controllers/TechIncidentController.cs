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
            return View(new TechSelectViewModel { Technicians = technicians });
        }


        [HttpGet]
        public IActionResult List(int id)
        {
            if (id != 0)
            {
                var session = new SportsProSession(HttpContext.Session);
                session.setMyTech(id);

                var technician = context.Technicians.Find(id);
                var incidents = context.Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .Where(i => i.TechnicianID == technician.TechnicianID && i.DateClosed == null)
                    .ToList();

                var model = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incidents = incidents,
                    SelectedTechnicianId = id
                };

                if (model.Incidents.Count == 0)
                {
                    TempData["message"] = $"No open incidents for this Technician";
                }

                return View(model);
            }
            else
            {
                TempData["message"] = $"Please select a Technician";
                return RedirectToAction("Get");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Model = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .SingleOrDefault(i => i.IncidentID == id);
            var session = new SportsProSession(HttpContext.Session);
            return View(Model);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            var i = context.Incidents.Find(incident.IncidentID);
            i.Description = incident.Description;
            i.DateClosed = incident.DateClosed;

            context.Incidents.Update(i);
            context.SaveChanges();
            return RedirectToAction("List", new { id = incident.TechnicianID });
        }
    }
}
