
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExotikaTrial2.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(50, ErrorMessage = "Employee Name cannot take more than 50 characters.")]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public string? ResortId { get; set; }
        [ForeignKey("ResortId")]
        [ValidateNever]
        public Resort ResortDetails { get; set; }

        [Required]
        public string Designation { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Joining")]
        public DateTime DateOfJoining { get; set; }

        public int Salary { get; set; }
    }
}
