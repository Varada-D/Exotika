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


        public Resort? resort { get; set; }
        public Vendor? vendor { get; set; }
        public Tourist? tourist { get; set; }
        public HandicraftDealer? handicraftDealer { get; set; }


        [Range(1,5)]
        public int Rating { get; set; }

        [Display(Name ="Feedback / Message")]
        public string FeedbackMsg { get; set; }

        [DataType(DataType.Date)]
        public DateTime lastUpdated { get; set; }
    }
}
