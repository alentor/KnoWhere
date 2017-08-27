using Communication;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser.Results
{
    public class GooglePlaceDetailsResult
    {
        public bool IsSucess { get; set; } = true;
        public PlaceDetails PlaceDetails { get; set; }
    }
}