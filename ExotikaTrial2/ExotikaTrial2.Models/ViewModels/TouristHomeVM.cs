using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Models.ViewModels
{
    public class TouristHomeVM
    {
        public IEnumerable<Resort> Resorts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        // public Festival Festival { get; set; }
    }
}
