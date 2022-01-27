using System.ComponentModel.DataAnnotations;

namespace ExotikaTrial2.Models
{
	public class HandicraftDealer
	{
        [Key]
		public string HandicraftDealerId { get; set; }

        [Required]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [Display(Name = "Owner Name")]
        public string? OwnerName { get; set; }

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
