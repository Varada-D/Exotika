using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Models
{
    public class Booking
    {

		[Key]
		public int BookingID { get; set; }

		public int noOfBookings { get; set; }
		public string idType { get; set; }
		public string idNumber { get; set; }


		public int TotalPrice { get; set; }

		[DataType(DataType.Date)]
		public DateTime CheckInDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime CheckOutDate { get; set; }

		public string? ResortId { get; set; }
		[ForeignKey("ResortId")]
		[ValidateNever]
		public Resort Resort { get; set; }

		public string? TouristId { get; set; }
		[ForeignKey("TouristId")]
		[ValidateNever]
		public Tourist TouristDetails { get; set; }
		public int? PackageId { get; set; }
		[ForeignKey("PackageId")]
		[ValidateNever]
		public Package Package { get; set; }
	}
}
