using System;

namespace Communication
{
    public class ActivityRequest : RequestBase
    {
        public TimeOfDay TimeOfDay { get; set; }

        public Location UserLocation { get; set; }

        public int Radius { get; set; }
        
    }
}