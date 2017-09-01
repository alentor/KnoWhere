using Communication;
using System;
using System.Diagnostics;
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
            var httpAddress = AppSettings.GenerateWazeUri
            (
               placeName: places[currentPlaceIndex].Name,  
               latitude:  placeDetails.Location.Latitude,
               longitude: placeDetails.Location.Longitude
            );
            Device.OpenUri(httpAddress);
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

            try
            {
                var httpAddress = AppSettings.GetValue("PlaceDetailsRequestApi");
                placeDetails = (PlaceDetails)(await placeDetailsRequest.SendAsync(httpAddress));

                if (placeDetails == null)
                {
                    var missingDetailsMsg = new Label
                    {
                        Text = "I'm sorry but " + places[currentPlaceIndex].Name + " didn't provide their contact details",
                        Style = Application.Current.Resources["DefaultLabelStyle"] as Style
                    };

                    MainPanel.Children.Add(missingDetailsMsg);
                    await AddLoaderToView(MainPanel, 2000);

                    var additionalMsg = new Label
                    {
                        Text = "I'll look for some other places you can go...",
                        Style = Application.Current.Resources["DefaultLabelStyle"] as Style
                    };

                    MainPanel.Children.Add(additionalMsg);
                    await AddLoaderToView(MainPanel, 2000);
                    currentPlaceIndex++;
                    CreatePlaceSuggestion(MainPanel);
                }
                else
                {
                    CreatePlaceDetailsSuggestion(MainPanel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void MainPanel_SizeChanged(object sender, EventArgs e)
        {
            // Supports only Android
            await MainScroller.ScrollToAsync(0, MainPanel.Height, true);

            // Supports only IOS
            //await MainScroller.ScrollToAsync(0, MainScroller.Content.Height, true);
        }

    }
}