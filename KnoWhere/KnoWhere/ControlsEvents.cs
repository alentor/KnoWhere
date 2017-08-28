using Communication;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {  
        private void CallBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Phone != null)
                Device.OpenUri(new Uri("tel: " + placeDetails.Phone));
            else
                DisplayAlert("Alert", places[currentPlaceIndex].Name + " didn't provide their phone number", "OK");
        }

        private void VisitWebsiteBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Website != null)
                Device.OpenUri(placeDetails.Website);
            else
                DisplayAlert("Alert", places[currentPlaceIndex].Name + " don't own a website", "OK");
        }

        private void NavigateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Location != null) 
                Device.OpenUri(new Uri("https://waze.com/ul?ll=" + placeDetails.Location.Latitude + '&' + placeDetails.Location.Longitude));
            else
                DisplayAlert("Alert", places[currentPlaceIndex].Name + " didn't provide their location", "OK");
        }

        private void NextBtn_Clicked(object sender, EventArgs e)
        {
            currentPlaceIndex++;
            Button button = (Button)sender;
            DisableParent(button);

            CreatePlaceSuggestion(MainPanel);
        }

        private async void interestedBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DisableParent(button);

            var placeChosen = places[currentPlaceIndex];
            var placeDetailsRequest = new PlacesDetailsRequest { PlaceId = placeChosen.Id };

            placeDetails = (PlaceDetails)(await placeDetailsRequest.SendAsync());
            CreatePlaceDetailsSuggestion(MainPanel);
        }

        private async Task<List<Place>> GeneratePlaces()
        {
            // Adding loader gif
            AddLoaderToView(MainPanel);

            double? longitude = null;
            double? latitude = null;

            // Getting user current location if GPS is on
            if (CrossGeolocator.Current.IsGeolocationEnabled)
            {
                var position = await CrossGeolocator.Current.GetPositionAsync(null, null, false);
                longitude = position.Longitude;
                latitude = position.Latitude;
            }

            // Creating the request
            var request = new PlacesRequest()
            {
                Language = EnumExtensions.GetDisplayName(Language.English).ToString(),
                Location = !(longitude.HasValue && latitude.HasValue) ? null :
                new Location
                {
                    Latitude = latitude.Value,
                    Longitude = longitude.Value
                }
            };

            // Removing loader gif
            RemoveLoaderFromView(MainPanel);

            return (List<Place>)request.Send();

        }

        private async void MainPanel_SizeChanged(object sender, EventArgs e)
        {
            // Supports only Android
            await MainScroller.ScrollToAsync(0, MainScroller.Content.Height, true);

            // Supports only IOS
            //await MainScroller.ScrollToAsync(0, MainScroller.Content.Height, true);
        }

    }
}