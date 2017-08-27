using System;

namespace Communication
{
    public class PlaceDetails : PlaceBase
    {
        public string Phone { get; set; }
        public Uri Website { get; set; }
        public Location Location { get; set; }
    }
}