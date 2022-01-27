using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = SD.Role_User_Resort)]
    public class Requirement
    {
        [Key]
        public int RequirementId { get; set; }

        public string? ResortId { get; set; }
        [ForeignKey("ResortId")]
        [ValidateNever]
        public Resort Resort { get; set; }

        public string Type { get; set; }
        public string Category { get; set; }

        [Display(Name = "Requirement")]
        public string? requirementName { get; set; }

        public string? Description { get; set; }

        [Range(1,int.MaxValue)]
        public int? Quantity { get; set; }


        [Display(Name ="Duration (in months)"), Range(1,int.MaxValue)]
        public int? Duration { get; set; }


        [DataType(DataType.Date)]
        public DateTime lastUpdated { get; set; }


        [DataType(DataType.Date), Display(Name = "Required By")]
        public DateTime requiredBy { get; set; }


        public string? Status { get; set; }
    }
}
