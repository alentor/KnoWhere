using System.IO;
using System.Net;
using System.Threading.Tasks;
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
    public class PlaceDetailsController : Controller
    {
        private readonly Settings _Settings;
        private readonly GoogleJsonParser _GoogleJsonParser = new GoogleJsonParser();

        // Controller constructor.
        public PlaceDetailsController(IOptions<Settings> optionsAccessor)
        {
            _Settings = optionsAccessor.Value;
        }

        [HttpGet("{id}")]
        public async Task<ContentResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Content(JsonConvert.SerializeObject(new PlaceDetailsResponse { IsSucess = false }), "application/json");
            string googleApiUrl = $"https://maps.googleapis.com/maps/api/place/details/json?key={_Settings.PlacesApiKey}";
            GooglePlaceDetailsResult googlePlaceDetailsResult;
            WebRequest googlePlaceDetailRequest = WebRequest.Create($"{googleApiUrl}&placeid={id}");
            using (WebResponse webResponse = await googlePlaceDetailRequest.GetResponseAsync())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonResponse = streamReader.ReadToEnd();
                    googlePlaceDetailsResult = await _GoogleJsonParser.ParsePlacesDetailsAsync(jsonResponse);
                }
                if (!googlePlaceDetailsResult.IsSucess) return Content(JsonConvert.SerializeObject(new PlaceDetailsResponse { IsSucess = false }), "application/json");
                PlaceDetailsResponse response = new PlaceDetailsResponse
                {
                    Place = googlePlaceDetailsResult.PlaceDetails,
                    IsSucess = true
                };
                return Content(JsonConvert.SerializeObject(response), "application/json");
            }
        }
    }
}