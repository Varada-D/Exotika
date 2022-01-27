using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExotikaTrial2.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DisplayName("Price Per Day Per Head")]
        public int Price { get; set; }

        public string? ResortId { get; set; }
    }
}
