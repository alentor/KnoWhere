using System.Collections.Generic;

namespace Communication
{
    public static class AppSettings
    {
        public static Dictionary<string, string> Settings = new Dictionary<string, string>()
        {
            // KayValue Pairs
            { "PlaceRequestApi",        "http://www.igal-testing-sites.com/api/places?" },
            { "PlaceDetailsRequestApi", "http://www.igal-testing-sites.com/api/placedetails/" },
            { "ImageRequestApi",        "http://www.igal-testing-sites.com/api/image/" }
        };
    }
}
