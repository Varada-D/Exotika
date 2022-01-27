using System.ComponentModel.DataAnnotations;

namespace ExotikaTrial2.Models
{
	public class Tourist
	{
        [Key]
		public string TouristId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

		[DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? emailAddr { get; set; }

        [DataType(DataType.Date)]
        public DateTime? createDate { get; set; }
    }
}
