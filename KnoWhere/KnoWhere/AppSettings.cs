using System;
using System.Collections.Generic;

namespace KnoWhere
{
    public static class AppSettings
    {
        private static Dictionary<string, string> Settings = new Dictionary<string, string>()
        {
            // KayValue Pairs
            { "PlaceRequestApi",        "http://www.igal-testing-sites.com/api/places?" },
            { "PlaceDetailsRequestApi", "http://www.igal-testing-sites.com/api/placedetails/" },
            { "ImageRequestApi",        "http://www.igal-testing-sites.com/api/image/" }
        };

        public static string GetValue(string key)
        {
            if (String.IsNullOrEmpty(Settings[key]))
                throw new ApplicationException(key + " was not found in Application Settings");

            return Settings[key];
        }

    }
}
