using System.ComponentModel.DataAnnotations;

namespace ExotikaTrial2.Models
{
	public class Vendor
	{
		[Key]
		public string VendorId { get; set; }

		[Required]
		[Display(Name = "Business Name")]
		public string Name { get; set; }

		[Display(Name = "Name of the Vendor")]
		public string? VendorName { get; set; }

		[Display(Name = "Street Address")]
		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }

		[Display(Name = "Postal Code")]
		public string? PostalCode { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }

		[DataType(DataType.EmailAddress)]
		public string? emailAddr { get; set; }

		[DataType(DataType.Date)]
		public DateTime? createDate { get; set; }
	}
}
