using System.IO;
using System.Net;
using System.Threading.Tasks;
using Communication;
using Communication.Response;
using KnoWhere.API.Config;
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
        private readonly Settings _settings;
        private readonly GoogleJsonParser _GoogleJsonParser = new GoogleJsonParser();

        // Places controller constructor.
        public PlacesController(IOptions<Settings> optionsAccessor)
        {
            _settings = optionsAccessor.Value;
        }

        // GET api/Places
        // E.G http://localhost:10607/api/places?Language=en&location.Latitude=32.07861&location.Longitude=34.881487
        [HttpGet]
        public async Task<ContentResult> Get(PlacesRequest request)
        {
            string asd = _settings.PlacesApiKey;
            if (!ModelState.IsValid) return Content(JsonConvert.SerializeObject(new PlacesResponse { isSucess = false }), "application/json");
            GooglePlacesResult googlePlacesResult;
            WebRequest webRequest = WebRequest.Create($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={_settings.PlacesApiKey}&location={request.Location.Latitude},{request.Location.Longitude}&radius=2000");
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                if (webResponse.GetResponseStream() == null)
                    return Content(JsonConvert.SerializeObject(new PlacesResponse { isSucess = false }), "application/json");
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonResponse = streamReader.ReadToEnd();
                    googlePlacesResult = await _GoogleJsonParser.ParsePlacesAsync(jsonResponse);
                }
            }
            if (!googlePlacesResult.IsSucess) return Content(JsonConvert.SerializeObject(new PlacesResponse { isSucess = false }), "application/json");
            PlacesResponse response = new PlacesResponse
            {
                BucketId = "place holder",
                PLaces = googlePlacesResult.Places
            };
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}