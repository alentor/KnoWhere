using System;

namespace Communication
{
    public class PlaceDetails : PlaceBase
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public Uri Website { get; set; }
        public Location Location { get; set; }
    }
}