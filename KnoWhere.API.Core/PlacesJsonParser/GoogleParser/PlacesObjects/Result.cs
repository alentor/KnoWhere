using System.Collections.Generic;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects {
    public class Result {
        public Geometry Geometry {get; set;}
        public string Icon {get; set;}
        public string Id {get; set;}
        public string Name {get; set;}
        public OpeningHours OpeningHours {get; set;}
        public List <Photo> Photos {get; set;}
        public string PlaceId {get; set;}
        public double Rating {get; set;}
        public string Reference {get; set;}
        public string Scope {get; set;}
        public List <string> Types {get; set;}
        public string Vicinity {get; set;}
        public int? PriceLevel {get; set;}
    }
}