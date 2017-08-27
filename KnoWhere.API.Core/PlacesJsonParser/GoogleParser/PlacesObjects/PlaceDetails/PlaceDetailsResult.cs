using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.PlaceDetails
{
    public class PlaceDetailsResult
    {
        [JsonProperty("html_attributions")]
        public List<object> HtmlAttributions { get; set; }
        [JsonProperty("result")]
        public PlaceDetailsGoogle PlaceDetailsGoogle { get; set; }
        public string Status { get; set; }
    }
}