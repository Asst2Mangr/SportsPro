using Microsoft.AspNetCore.Mvc;
using SportsPro.Models; 
using System.Linq;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly SportsProContext _context;

        public TechnicianController(SportsProContext context)
        {
            _context = context;
        }

        // Display Technician Manager page
        public IActionResult List()
        {
            var technicians = _context.Technicians.ToList();
            return View(technicians);
        }

        // Display Add/Edit Technician page with blank fields
        [HttpGet]
        public IActionResult Add()
        {
            return View("Edit", new Technician());
        }

        // Display Add/Edit Technician page with current data for technician
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound(); // technician is not found
            }
            return View("Edit", technician);
        }

        [HttpPost]
        // Handle submission for adding/editing a technician
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    _context.Technicians.Add(technician);
                }
                else
                {
                    _context.Technicians.Update(technician);
                }
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View("Edit", technician);
        }

        // Display the Delete Technician page that confirms the deletion
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician == null)
            {
                return NotFound(); // Handle when technician is not found
            }
            return View("Delete", technician);
        }

        [HttpPost]
        // Handle submission for confirming deletion
        public IActionResult DeleteConfirmed(Technician model)
        {
            var technician = _context.Technicians.Find(model.TechnicianID);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
