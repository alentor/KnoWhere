using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Communication
{
    public class PlacesRequest : IRequest
    { 

        public Location Location { get; set; }

        public string Language { get; set; }

        public async Task<object> Send()
        {
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                var queryString = ToQueryString();
                var jsonResponse = await httpClient.GetStringAsync("http://79.176.58.22/api/places?" + queryString);

                // Serializing to Jobject
                var jsonObj = JObject.Parse(jsonResponse);

                // Getting places from JArray
                var placesJArr = jsonObj["Places"].ToString();

                // Deserializing JsonArray to Place List
                var places = JsonConvert.DeserializeObject<List<Place>>(placesJArr);

                return places;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public string ToQueryString()
        {
            return "Language=" + Language + "&location.Latitude=" + Location.Latitude + "&location.Longitude=" + Location.Longitude;
        }
         
    } 
}