using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class PlaceDetails : PlaceBase
    { 
        public string Phone { get; set; }

        public Uri Website { get; set; }
        
        public Location Location { get; set; }
    }
}
