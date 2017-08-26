using System;
using System.Threading.Tasks;
using Communication;
using KnoWhere.API.Core.PlacesJsonParser.GoogleParser.PlacesObjects;
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
                GooglePlaces googlePlaceses = await Task.Run(() => JsonConvert.DeserializeObject<GooglePlaces>(json));
                if(googlePlaceses.Status != "OK")
                    result.IsSucess = false;
                foreach (Result googlePlace in googlePlaceses.Results) result.Places.Add(ConvertToPlace(googlePlace));
                return result;
            }
            catch (Exception e)
            {
                result.IsSucess = false;
                Console.WriteLine(e);
                throw;
            }
        }

        private static Place ConvertToPlace(Result result)
        {
            Place place = new Place
            {
                Id = result.Id,
                Address = result.Vicinity,
                Name = result.Name,
                Rating = result.Rating,
            };
            return place;
        }
    }
}