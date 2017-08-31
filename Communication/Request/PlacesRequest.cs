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
    public class PlacesRequest : RequestBase, IRequest
    {
        public Location Location { get; set; }
        public string Language { get; set; } 
         

        public object Send(string HttpAddress)
        { 
            try
            { 
                using (var client = new WebClient())
                {
                    var queryString = ToQueryString();
                    var jsonResponse = client.DownloadString(HttpAddress + queryString);

                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting places from JArray
                    var placesJArr = jsonObj["Places"].ToString();

                    // Deserializing JsonArray to Place List
                    ResponseData = JsonConvert.DeserializeObject<List<Place>>(placesJArr); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No places were found. ex: " + ex.ToString());
            }

            return ResponseData;
        }

        public async Task<object> SendAsync(string HttpAddress)
        { 
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                var queryString = ToQueryString();
                var jsonResponse = await httpClient.GetStringAsync(ApiUri + queryString);

                // Serializing to Jobject
                var jsonObj = JObject.Parse(jsonResponse);

                // Getting places from JArray
                var placesJArr = jsonObj["Places"].ToString();

                // Deserializing JsonArray to Place List
                ResponseData = JsonConvert.DeserializeObject<List<Place>>(placesJArr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No places were found. ex: " + ex.ToString());
            }

            return ResponseData; 
        }

        public string ToQueryString()
        {
            return "Language=" + Language + "&location.Latitude=" + Location.Latitude + "&location.Longitude=" + Location.Longitude;
        }
         
    }
}