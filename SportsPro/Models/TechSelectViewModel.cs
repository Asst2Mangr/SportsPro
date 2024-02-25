using System.Collections.Generic;

namespace SportsPro.Models
{
    public class TechSelectViewModel
    {
        public List<Technician> Technicians { get; set; }
        public int SelectedTechnicianId { get; set; }
    }

}
