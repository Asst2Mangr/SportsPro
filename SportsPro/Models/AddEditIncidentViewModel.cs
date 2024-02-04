using System.Collections.Generic;

namespace SportsPro.Models
{
    public class AddEditIncidentViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Technician> Technicians { get; set; }
        public Incident CurrentIncident { get; set; }
        public string Operation { get; set; } //for add or edit
    }
}