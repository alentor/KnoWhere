using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects
{
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool IsOpenNow { get; set; }
        [JsonProperty("weekday_text")]
        public List<object> WeekdayText { get; set; }
    }
}