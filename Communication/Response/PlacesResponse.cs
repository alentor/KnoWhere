using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Response
{
    public class PlacesResponse {
        public bool isSucess {get; set;} = true;
        public List <Place> Places { get; set; }
        public string BucketId {get; set;}
    }
}
