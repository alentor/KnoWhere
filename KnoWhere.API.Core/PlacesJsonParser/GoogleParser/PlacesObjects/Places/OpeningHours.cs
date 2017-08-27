using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.Places
{
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
        [JsonProperty("weekday_text")]
        public List<object> WeekdayText { get; set; }
    }
}