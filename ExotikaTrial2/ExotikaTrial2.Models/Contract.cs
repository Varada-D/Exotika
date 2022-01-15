using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExotikaTrial2.Models
{
	public class Contract
	{
		[Key]
		public int ContractId { get; set; }

		public string? VendorId { get; set; }
		[ForeignKey("VendorId")]
		[ValidateNever]
		public Vendor Vendor { get; set; }

		public string? ResortId { get; set; }
		[ForeignKey("ResortId")]
		[ValidateNever]
		public Resort Resort { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }

		public string Summary { get; set; }

		[DataType(DataType.Upload)]
		public string ProposalFile { get; set; }
	}
}
