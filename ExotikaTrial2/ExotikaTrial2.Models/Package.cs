using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExotikaTrial2.Models
{
    public class Package
    {
        [Key]
        public string PackageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DisplayName("Price Per Day Per Head")]
        public int Price { get; set; }

        public string? ResortId { get; set; }
        [ForeignKey("ResortId")]
        [ValidateNever]
        public Resort Resort { get; set; }
    }
}
