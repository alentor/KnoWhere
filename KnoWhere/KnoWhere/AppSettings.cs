using System;
using System.Collections.Generic;
using System.Net;

namespace KnoWhere
{
    public static class AppSettings
    {
        private static Dictionary<string, string> Settings = new Dictionary<string, string>()
        {
            // KayValue Pairs
            { "PlaceRequestApi",        "http://www.igal-testing-sites.com/api/places?" },
            { "PlaceDetailsRequestApi", "http://www.igal-testing-sites.com/api/placedetails/" },
            { "ImageRequestApi",        "http://www.igal-testing-sites.com/api/image/" },
            { "WazeRequestApi",         "https://waze.com/ul?q=" }
        };

        public static string GetValue(string key)
        {
            if (String.IsNullOrEmpty(Settings[key]))
                throw new ApplicationException(key + " was not found in Application Settings");

            return Settings[key];
        }

        public static Uri GenerateWazeUri(string placeName, double latitude, double longitude)
        {
            var httpAddress = GetValue("WazeRequestApi");
            string urlEncodedQuery = WebUtility.UrlEncode(placeName).Replace("+", "%20");
            string finalUrl = httpAddress + urlEncodedQuery + "&ll=" + latitude + "&" + longitude + "&" + "navigate=yes";
            return new Uri(finalUrl);
        }

    }
}
