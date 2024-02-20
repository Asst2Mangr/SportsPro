using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class TechIncidentViewModel : Controller
    {
        public Technician Technician { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
