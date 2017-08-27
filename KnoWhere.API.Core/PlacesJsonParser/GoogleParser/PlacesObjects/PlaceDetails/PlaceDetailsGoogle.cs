using System.Collections.Generic;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.Common;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.PlaceDetails
{
    public class PlaceDetailsGoogle
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }
        [JsonProperty("adr_address")]
        public string Address { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("formatted_phone_number")]
        public string FormattedPhoneNumber { get; set; }
        public Geometry Geometry { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        [JsonProperty("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }
        public string Name { get; set; }
        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }
        public List<Photo> Photos { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        public double Rating { get; set; }
        public string Reference { get; set; }
        public List<Review> Reviews { get; set; }
        public string Scope { get; set; }
        public List<string> Types { get; set; }
        public string Url { get; set; }
        [JsonProperty("utc_offset")]
        public int UtcOffset { get; set; }
        public string Vicinity { get; set; }
        public string Website { get; set; }
    }
}