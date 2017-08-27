using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using ModernHttpClient;

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

                    // Deserializing JsonArray to Place List
                    details = JsonConvert.DeserializeObject<PlaceDetails>(jsonResponse);
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
                    details = await httpClient.GetStringAsync(new Uri("http://79.176.58.22/api/placedetails/" + PlaceId));
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return details;
        }
    }
}