using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using KnoWhere.API.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace KnoWhere.API.Controllers
{
    [Route("api/[controller]")]

    public class ImageController : Controller
    {
        private readonly Settings _Settings;

        // Controller constructor.
        public ImageController(IOptions<Settings> optionsAccessor)
        {
            _Settings = optionsAccessor.Value;
        }
        // GET api/image/{imageId}
        // E.G. http://localhost:10607/api/image/CnRtAAAATLZNl354RwP_9UKbQ_5Psy40texXePv4oAlgP4qNEkdIrkyse7rPXYGd9D_Uj1rVsQdWT4oRz4QrYAJNpFX7rzqqMlZw2h2E2y5IKMUZ7ouD_SlcHxYq1yL4KbKUv3qtWgTK0A6QbGh87GB3sscrHRIQiG2RrmU_jF4tENr9wGS_YxoUSSDrYjWmrNfeEHSGSc3FyhNLlBU
        // Returns the requested image.
        [HttpGet("{id}")]
        public async Task <ActionResult> Get (string id){
            if (string.IsNullOrEmpty(id))
                return null;
            string googleApiUrl = $"https://maps.googleapis.com/maps/api/place/photo?key={_Settings.PlacesApiKey}";
            WebRequest googlePlacesEntertainmentWebRequest = WebRequest.Create($"{googleApiUrl}&maxwidth=400&photoreference={id}");
            WebResponse webResponse = await googlePlacesEntertainmentWebRequest.GetResponseAsync();
            return File(webResponse.GetResponseStream(), webResponse.ContentType);
        }
    }
}
