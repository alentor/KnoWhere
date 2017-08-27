using System.Collections.Generic;

namespace Communication
{
    public class PlacesResponse
    {
        public bool IsSucess { get; set; } = true;
        public List<Place> Places { get; set; }
        public string BucketId { get; set; }
    }
}
