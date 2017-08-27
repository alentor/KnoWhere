namespace Communication.Response
{
    public class PlaceDetailsResponse
    {
        public bool IsSucess { get; set; } = true;
        public PlaceDetails Place { get; set; }
    }
}