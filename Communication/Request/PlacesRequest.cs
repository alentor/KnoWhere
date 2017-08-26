using System;

namespace Communication
{
    public class PlacesRequest
    { 

        public Location Location { get; set; }

        public string Language { get; set; }



        public string ToQueryString()
        {
            return "Language=" + Language + "&location.Latitude=" + Location.Latitude + "&location.Longitude=" + Location.Longitude;
        }
         
    }
}