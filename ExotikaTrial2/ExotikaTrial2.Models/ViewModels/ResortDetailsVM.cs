using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Models.ViewModels
{
    public class ResortDetailsVM
    {
        public Resort resort { get; set; }
        public IEnumerable<Package> packages { get; set; }
    }
}
