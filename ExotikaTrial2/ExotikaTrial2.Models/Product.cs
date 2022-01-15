using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExotikaTrial2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DisplayName("Price Per Day Per Head")]
        public int Price { get; set; }

        public int? HandicraftDealerId { get; set; }
        [ForeignKey("HandicraftDealerId")]
        [ValidateNever]
        public HandicraftDealer HandicraftDealer { get; set; }
    }
}
