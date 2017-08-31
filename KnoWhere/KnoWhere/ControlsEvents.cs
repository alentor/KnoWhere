using Communication;
using System;
using Xamarin.Forms;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {  
        private async void CallBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Phone != null)
                Device.OpenUri(new Uri("tel: " + placeDetails.Phone));
            else
                await DisplayAlert("Alert", places[currentPlaceIndex].Name + " didn't provide their phone number", "OK");
        }

        private async void VisitWebsiteBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Website != null)
                Device.OpenUri(placeDetails.Website);
            else
                await DisplayAlert("Alert", places[currentPlaceIndex].Name + " don't own a website", "OK");
        }

        private async void NavigateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (placeDetails.Location != null) 
                Device.OpenUri(new Uri("https://waze.com/ul?ll=" + placeDetails.Location.Latitude + '&' + placeDetails.Location.Longitude));
            else
                await DisplayAlert("Alert", places[currentPlaceIndex].Name + " didn't provide their location", "OK");
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
            var placeDetailsRequest = new PlacesDetailsRequest()
            {
                PlaceId = placeChosen.Id
            };

            placeDetails = (PlaceDetails)(await placeDetailsRequest.SendAsync());
            CreatePlaceDetailsSuggestion(MainPanel);
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