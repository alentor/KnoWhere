using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;

namespace Communication
{
    public class PlacesDetailsRequest : IRequest
    {
        public string PlaceId { get; set; }

        public async Task<object> Send()
        {
            object details = null;
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
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