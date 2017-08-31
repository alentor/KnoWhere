using Communication;
using System;
using Xamarin.Forms;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {  
        private void CallBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Device.OpenUri(new Uri("tel: " + placeDetails.Phone));
        }

        private void VisitWebsiteBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Device.OpenUri(placeDetails.Website);
        }

        private void NavigateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Device.OpenUri(new Uri("https://waze.com/ul?ll=" + placeDetails.Location.Latitude + '&' + placeDetails.Location.Longitude));
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

            var httpAddress = AppSettings.GetValue("PlaceDetailsRequestApi");
            placeDetails = (PlaceDetails)(await placeDetailsRequest.SendAsync(httpAddress));
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