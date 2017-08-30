using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json.Linq;

namespace Communication
{
    public class PlacesDetailsRequest : IRequest
    {
        public string PlaceId { get; set; }

        public object Send()
        {
            object details = null;
            try
            {
                using (var client = new WebClient())
                {
                    var jsonResponse = client.DownloadString(new Uri("http://79.176.58.22/api/placedetails/" + PlaceId));

                    
                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting place details from result
                    var placeDetails = jsonObj["Place"].ToString();

                    // Deserializing to PlaceDetails
                    details = JsonConvert.DeserializeObject<PlaceDetails>(placeDetails);
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return details;
        }

        public async Task<object> SendAsync()
        {
            object details = null;

            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());

                if (!String.IsNullOrEmpty(PlaceId))
                {
                    var jsonResponse = await httpClient.GetStringAsync(new Uri("http://79.176.58.22/api/placedetails/" + PlaceId));

                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting place details from result
                    var placeDetails = jsonObj["Place"].ToString();

                    // Deserializing to PlaceDetails
                    details = JsonConvert.DeserializeObject<PlaceDetails>(placeDetails);
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return details;
        }
    }
}