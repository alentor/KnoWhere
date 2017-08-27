using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects
{
    public class Result
    {
        public Geometry Geometry { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Photo> Photos { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        public double Rating { get; set; }
        public string Reference { get; set; }
        public string Scope { get; set; }
        public List<string> Types { get; set; }
        public string Vicinity { get; set; }
        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }
    }
}