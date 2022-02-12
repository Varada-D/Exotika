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

        [Required, DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DisplayName("Price Per Piece")]
        public int Price { get; set; }

        public string? HandicraftDealerId { get; set; }
        [ForeignKey("HandicraftDealerId")]
        [ValidateNever]
        public HandicraftDealer HandicraftDealer { get; set; }

        [DataType(DataType.Date)]
        public DateTime lastUpdated { get; set; }
    }
}
