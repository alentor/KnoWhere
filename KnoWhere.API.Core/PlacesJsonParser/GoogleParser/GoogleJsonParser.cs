using System;
using System.Threading.Tasks;
using Communication;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.PlaceDetails;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects.Places;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.Results;
using Newtonsoft.Json;

namespace KnoWhere.API.Core.PlacesJsonParser.GoogleParser
{
    public class GoogleJsonParser
    {
        public async Task<GooglePlacesResult> ParsePlacesAsync(string json)
        {
            GooglePlacesResult result = new GooglePlacesResult();
            try
            {
                PlacesResult placeses = await Task.Run(() => JsonConvert.DeserializeObject<PlacesResult>(json));
                if (placeses.Status != "OK")
                    result.IsSucess = false;
                foreach (PlaceGoogle googlePlace in placeses.PlacesGoogle)
                    if (googlePlace.Photos != null)
                        result.Places.Add(ConvertToPlace(googlePlace));
                result.BucketId = placeses.NextPageToken;
                return result;
            }
            catch
            {
                result.IsSucess = false;
                return result;
            }
        }

        public async Task<GooglePlaceDetailsResult> ParsePlacesDetailsAsync(string json)
        {
            GooglePlaceDetailsResult result = new GooglePlaceDetailsResult();
            try
            {
                PlaceDetailsResult placeDetails = await Task.Run(() => JsonConvert.DeserializeObject<PlaceDetailsResult>(json));
                if (placeDetails.Status != "OK")
                    result.IsSucess = false;
                result.PlaceDetails = ConvertToPlaceDetails(placeDetails.PlaceDetailsGoogle);
                return result;
            }
            catch
            {
                result.IsSucess = false;
                return result;
            }
        }

        private static Place ConvertToPlace(PlaceGoogle placeGoogle)
        {
            Place place = new Place
            {
                Id = placeGoogle.PlaceId,
                Address = placeGoogle.Vicinity,
                Name = placeGoogle.Name,
                Rating = placeGoogle.Rating,
            };
            if (placeGoogle.Photos != null)
                place.ImageId = placeGoogle.Photos[0].PhotoReference;
            return place;
        }

        private static PlaceDetails ConvertToPlaceDetails(PlaceDetailsGoogle placeDetailsesGoogle)
        {
            PlaceDetails placeDetails = new PlaceDetails
            {
                Id = placeDetailsesGoogle.PlaceId,
                Phone = placeDetailsesGoogle.FormattedPhoneNumber,
                Website = new Uri(placeDetailsesGoogle.Website),
                Location = new Location
                {
                    Latitude = placeDetailsesGoogle.Geometry.Location.Lat,
                    Longitude = placeDetailsesGoogle.Geometry.Location.Lng
                },
            };
            return placeDetails;
        }
    }
}