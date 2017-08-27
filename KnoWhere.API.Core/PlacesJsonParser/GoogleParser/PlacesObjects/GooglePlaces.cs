using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects
{
    public class GooglePlaces
    {
        [JsonProperty("html_attributions")]
        public List<object> HtmlAttributions { get; set; }
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }
        public List<Result> Results { get; set; }
        public string Status { get; set; }
    }
}