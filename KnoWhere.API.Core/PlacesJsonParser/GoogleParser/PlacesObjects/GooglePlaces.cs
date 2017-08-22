using System.Collections.Generic;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects {
    public class GooglePlaces {
        public List <object> Html_attributions {get; set;}
        public string Next_page_token {get; set;}
        public List <Result> Results {get; set;}
        public string Status {get; set;}
    }
}