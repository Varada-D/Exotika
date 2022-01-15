using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string byId { get; set; }
        public string forId { get; set; }

        [Range(1,5)]
        public double Rating { get; set; }

        [Display(Name ="Feedback / Message")]
        public string FeedbackMsg { get; set; }
    }
}
