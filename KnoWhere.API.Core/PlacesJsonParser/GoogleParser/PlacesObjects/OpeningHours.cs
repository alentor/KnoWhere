using System.Collections.Generic;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects {
    public class OpeningHours {
        public bool Open_now {get; set;}
        public List <string> Weekday_text {get; set;}
    }
}