using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required]
		[StringLength(51)]
		public string FirstName { get; set; }

		[Required]
        [StringLength(51)]
        public string LastName { get; set; }

		[Required]
        [StringLength(51)]
        public string Address { get; set; }

		[Required]
        [StringLength(51)]
        public string City { get; set; }

		[Required]
        [StringLength(51)]
        public string State { get; set; }

		[Required]
        [StringLength(21)]
        public string PostalCode { get; set; }

		[Required]
		public string CountryID { get; set; }
		public Country Country { get; set; }
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$")]
        public string Phone { get; set; }
		[Required]
        [StringLength(51)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}