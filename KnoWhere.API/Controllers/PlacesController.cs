using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Communication;
using KnoWhere.API.Config;
using KnoWhere.API.Core.ObjectExtensions;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnoWhere.API.Controllers
{
    [Route("api/[controller]")]
    public class PlacesController : Controller
    {
        private readonly Settings _Settings;
        private readonly GoogleJsonParser _GoogleJsonParser = new GoogleJsonParser();

        // Controller constructor.
        public PlacesController(IOptions<Settings> optionsAccessor)
        {
            _Settings = optionsAccessor.Value;
        }

        // GET api/Places
        // E.G http://localhost:10607/api/places?Language=en&location.Latitude=32.07861&location.Longitude=34.881487
        // Here we are doing 2 search places request to google api.
        // First request contains a search keyword 'entertainment' in order to receive places which offer some sort of entertainment.
        // Second request contains search type 'restaurant' in order to receive nearby restaurants.
        // Once two of the places requests returned a positive response, we shuffle two of the places lists into a single list of places, and return that list of places.
        [HttpGet]
        public async Task<ContentResult> Get(PlacesRequest request)
        {
            if (string.IsNullOrEmpty(request.Language) || request.Location == null)
                return Content(JsonConvert.SerializeObject(new PlacesResponse { IsSucess = false }), "application/json");
            string googleApiUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={_Settings.PlacesApiKey}&location={request.Location.Latitude},{request.Location.Longitude}&radius=2000";
            // Do Entertainment request to google places API.
            GooglePlacesResult googlePlacesEntertainmentResult;
            WebRequest googlePlacesEntertainmentWebRequest = WebRequest.Create($"{googleApiUrl}&keyword=entertainment");
            using (WebResponse webResponse = await googlePlacesEntertainmentWebRequest.GetResponseAsync())
            {
                if (webResponse.GetResponseStream() == null)
                    return Content(JsonConvert.SerializeObject(new PlacesResponse { IsSucess = false }), "application/json");
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonResponse = streamReader.ReadToEnd();
                    googlePlacesEntertainmentResult = await _GoogleJsonParser.ParsePlacesAsync(jsonResponse);
                }
            }
            // Do Restaurant request to google places API.
            GooglePlacesResult googlePlacesRestaurantResult;
            WebRequest googlePlacesRestaurantWebRequest = WebRequest.Create($"{googleApiUrl}&types=restaurant");
            using (WebResponse webResponse = await googlePlacesRestaurantWebRequest.GetResponseAsync())
            {
                if (webResponse.GetResponseStream() == null)
                    return Content(JsonConvert.SerializeObject(new PlacesResponse { IsSucess = false }), "application/json");
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonResponse = streamReader.ReadToEnd();
                    googlePlacesRestaurantResult = await _GoogleJsonParser.ParsePlacesAsync(jsonResponse);
                }
            }
            if (!googlePlacesEntertainmentResult.IsSucess || !googlePlacesRestaurantResult.IsSucess)
                return Content(JsonConvert.SerializeObject(new PlacesResponse { IsSucess = false }), "application/json");
            PlacesResponse response = new PlacesResponse
            {
                BucketId = "place holder",
                // Combine and suffle Entertainment and Restaurant places.
                Places = googlePlacesEntertainmentResult.Places.Concat(googlePlacesRestaurantResult.Places).ToList().Shuffle()
            };
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}