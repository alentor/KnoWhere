using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.Places
{
    public class PlacesResult
    {
        [JsonProperty("html_attributions")]
        public List<object> HtmlAttributions { get; set; }
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }
        [JsonProperty("results")]
        public List<PlaceGoogle> PlacesGoogle { get; set; }
        public string Status { get; set; }
    }
}