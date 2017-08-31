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
    public class PlacesDetailsRequest : RequestBase, IRequest
    {
        public string PlaceId { get; set; }  
      
         
        public object Send(string HttpAddress)
        { 
            try
            {
                using (var client = new WebClient())
                {
                    var jsonResponse = client.DownloadString(new Uri(HttpAddress + PlaceId));

                    
                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting place details from result
                    var placeDetails = jsonObj["Place"].ToString();

                    // Deserializing to PlaceDetails
                    ResponseData = JsonConvert.DeserializeObject<PlaceDetails>(placeDetails);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No place details were found. ex: " + ex.ToString());
            }

            return ResponseData;
        }

        public async Task<object> SendAsync(string HttpAddress)
        {
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());

                if (!String.IsNullOrEmpty(PlaceId))
                {
                    var jsonResponse = await httpClient.GetStringAsync(new Uri(HttpAddress + PlaceId));

                    // Serializing to Jobject
                    var jsonObj = JObject.Parse(jsonResponse);

                    // Getting place details from result
                    var placeDetails = jsonObj["Place"].ToString();

                    // Deserializing to PlaceDetails
                    ResponseData = JsonConvert.DeserializeObject<PlaceDetails>(placeDetails);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No place details were found. ex: " + ex.ToString());
            }

            return ResponseData;
        }
    }
}