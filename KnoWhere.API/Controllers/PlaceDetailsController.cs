using System.Threading.Tasks;
using Communication;
using Communication.Response;
using KnoWhere.API.Config;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser;
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

        [HttpGet]
        public async Task<ContentResult> Get(PlaceDetailsRequest request)
        {
            if (string.IsNullOrEmpty(request.PlaceId))
                return Content(JsonConvert.SerializeObject(new PlaceDetailsResponse { IsSucess = false }), "application/json");

            return null;
        }
    }
}
