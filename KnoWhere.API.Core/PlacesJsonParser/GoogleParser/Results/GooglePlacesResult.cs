using System.Collections.Generic;
using Communication;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.Results
{
    public class GooglePlacesResult {
        public bool IsSucess {get; set;} = true;
        public List<Place> Places { get; set; } = new List <Place>();
    }
}