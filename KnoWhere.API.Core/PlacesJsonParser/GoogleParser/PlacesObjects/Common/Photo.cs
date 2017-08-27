using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.Common
{
    public class Photo
    {
        public int Height { get; set; }
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributions { get; set; }
        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }
        public int Width { get; set; }
    }
}