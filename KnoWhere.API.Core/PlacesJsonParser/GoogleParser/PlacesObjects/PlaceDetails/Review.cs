using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.PlaceDetails
{
    public class Review
    {
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
        [JsonProperty("author_url")]
        public string AuthorUrl { get; set; }
        public string Language { get; set; }
        [JsonProperty("profile_photo_url")]
        public string ProfilePhotoUrl { get; set; }
        public int Rating { get; set; }
        [JsonProperty("relative_time_description")]
        public string RelativeTimeDescription { get; set; }
        public string Text { get; set; }
        public int Time { get; set; }
    }
}