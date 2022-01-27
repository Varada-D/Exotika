using System.ComponentModel.DataAnnotations;

namespace ExotikaTrial2.Models
{
	public class Resort
	{
		[Key]
		public string ResortId { get; set; }

        [Display(Name = "Business Name")]
		public string Name { get; set; }

        [Display(Name = "Owner Name")]
		public string? OwnerName { get; set; }

		[DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone NNumber")]
		public string PhoneNumber { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email Address")]
		public string emailAddr { get; set; }

		[Display(Name = "Street Address")]
		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		[Display(Name = "Postal Code")]
		public string? PostalCode { get; set; }

		[DataType(DataType.Date)]
		public DateTime? createDate { get; set; }
	}
}
