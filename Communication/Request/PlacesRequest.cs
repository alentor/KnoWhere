using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Communication
{
    public class PlacesRequest : IRequest
    {
        public Location Location { get; set; }
        public string Language { get; set; }

        public object Send()
        {
            object places = null;

            try
            {

                var tt = AppSettings.Settings["PlaceRequestApi"];

                using (var client = new WebClient())
                {
                    var queryString = ToQueryString();
                    var jsonResponse = client.DownloadString(AppSettings.Settings["PlaceRequestApi"] + queryString);

                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting places from JArray
                    var placesJArr = jsonObj["Places"].ToString();

                    // Deserializing JsonArray to Place List
                    places = JsonConvert.DeserializeObject<List<Place>>(placesJArr); 
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No places was found. ex: " + ex.ToString());
            }

            return places;
        }

        public async Task<object> SendAsync()
        {
            object places = null;

            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                var queryString = ToQueryString();
                var jsonResponse = await httpClient.GetStringAsync(AppSettings.Settings["PlaceRequestApi"] + queryString);

                // Serializing to Jobject
                var jsonObj = JObject.Parse(jsonResponse);

                // Getting places from JArray
                var placesJArr = jsonObj["Places"].ToString();

                // Deserializing JsonArray to Place List
                places = JsonConvert.DeserializeObject<List<Place>>(placesJArr);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No places was found. ex: " + ex.ToString());
            }

            return places; 
        }

        public string ToQueryString()
        {
            return "Language=" + Language + "&location.Latitude=" + Location.Latitude + "&location.Longitude=" + Location.Longitude;
        }
         
    }
}