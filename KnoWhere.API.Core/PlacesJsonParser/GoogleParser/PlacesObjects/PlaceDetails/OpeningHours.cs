using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.PlaceDetails
{
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
        public List<Period> Periods { get; set; }
        [JsonProperty("weekday_text")]
        public List<string> WeekdayText { get; set; }
    }
}