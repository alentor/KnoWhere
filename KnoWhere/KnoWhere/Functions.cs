using Communication;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {
        private async void CreatePlaceSuggestion(View layout)
        {
            // Adding loader gif
            await AddLoaderToView(MainPanel);

            // Casting view to layout panel
            var stackLayout = layout as StackLayout;

            var place = places[currentPlaceIndex];

            Image image = new Image
            {
                Style = Application.Current.Resources["DefaultImageStyle"] as Style
            };

            // Retrieving the image from api
            if (!String.IsNullOrEmpty(place.ImageId))
            {
                ImageRequest imageRequest = new ImageRequest()
                {
                    ImageId = place.ImageId
                };

                var httpAddress = AppSettings.GetValue("ImageRequestApi");
                var imageResponse = (Stream)(await imageRequest.SendAsync(httpAddress));
                var imageSource = ImageSource.FromStream(() => imageResponse);
                image.Source = imageSource;
            }

            Label suggestion = new Label
            {
                Text = "How about " + place.Name + "?",
                Style = Application.Current.Resources["DefaultLabelStyle"] as Style
            };

            List<View> suggestionList = new List<View>()
            {
                suggestion,
                image
            };

            // Creating panel for suggestion
            var suggestionLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };

            Button nextBtn = new Button()
            {
                Text = "Next",
                Style = Application.Current.Resources["DefaultButtonStyle"] as Style
            };

            Button interestedBtn = new Button()
            {
                Text = "Interested",
                Style = Application.Current.Resources["DefaultButtonStyle"] as Style
            };

            List<View> buttons = new List<View>()
            {
                nextBtn,
                interestedBtn,
            };

            // Subscribe to click event 
            nextBtn.Clicked += NextBtn_Clicked;
            interestedBtn.Clicked += interestedBtn_Clicked;

            // Creating panel for the buttons
            var buttonsLayout = new StackLayout()
            { 
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };

            // Removing loading gif 
            RemoveLoaderFromView(MainPanel);

            // Adding suggestion to layout
            AddItemsToView(suggestionLayout, suggestionList);
            stackLayout.Children.Add(suggestionLayout);

            // Adding buttons to layout
            AddItemsToView(buttonsLayout, buttons);
            stackLayout.Children.Add(buttonsLayout);
        }

        private async void CreatePlaceDetailsSuggestion(View layout)
        {
            // Adding loader gif
            await AddLoaderToView(MainPanel);

            // Casting view to layout panel
            var stackLayout = layout as StackLayout;

            List<View> buttons = new List<View>();

            Label suggestion = new Label
            {
                Text = "What would you like to do ?",
                Style = Application.Current.Resources["DefaultLabelStyle"] as Style
            };

            // Ensure phone has value before adding a button
            if (!String.IsNullOrEmpty(placeDetails.Phone))
            {
                Button callBtn = new Button()
                {
                    Text = "Call",
                    Style = Application.Current.Resources["DefaultButtonStyle"] as Style
                };
                callBtn.Clicked += CallBtn_Clicked;
                buttons.Add(callBtn);
            }

            // Ensure location has value before adding a button
            if (placeDetails.Location != null)
            {
                Button navigateBtn = new Button()
                {
                    Text = "Navigate Me",
                    Style = Application.Current.Resources["DefaultButtonStyle"] as Style
                };
                navigateBtn.Clicked += NavigateBtn_Clicked;
                buttons.Add(navigateBtn);
            }

            // Ensure website has value before adding a button
            if (placeDetails.Website != null)
            {
                Button visitWebsiteBtn = new Button()
                {
                    Text = "Visit Website",
                    Style = Application.Current.Resources["DefaultButtonStyle"] as Style
                };
                visitWebsiteBtn.Clicked += VisitWebsiteBtn_Clicked;
                buttons.Add(visitWebsiteBtn);
            }
             
            // Creating panel for the buttons
            var buttonsLayout = new StackLayout()
            { 
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };

            // Removing loading gif 
            RemoveLoaderFromView(MainPanel);

            // Adding suggestion msg
            stackLayout.Children.Add(suggestion);

            // Adding buttons to layout
            AddItemsToView(buttonsLayout, buttons);
            stackLayout.Children.Add(buttonsLayout);

            // Adding loader gif with waiting time 
            await AddLoaderToView(MainPanel, 10000);

            // Incrementing place index
            currentPlaceIndex++;

            // Creating another place suggestion
            CreatePlaceSuggestion(MainPanel);
        }
          
        private void AddItemsToView(Layout<View> layout, List<View> newItems)
        {
            foreach (var item in newItems)
            {
                layout.Children.Add(item);
            }
        }

        private async Task AddLoaderToView(View layout, int? milliseconds = null)
        {
            // Casting view to layout panel
            var stackLayout = layout as StackLayout;

            if (milliseconds.HasValue)
            {
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // Casting view to layout panel
                        MainPanel.Children.Add(loaderLayout);
                    });

                    // Holding background thread for specified milliseconds
                    Thread.Sleep(milliseconds.Value);
                });
                 
                // Removing the loader gif
                RemoveLoaderFromView(MainPanel);
            }
            else
            {
                stackLayout.Children.Add(loaderLayout);
            }
            
        }

        private void RemoveLoaderFromView(View layout)
        {
            // Casting view to layout panel
            var stackLayout = layout as StackLayout;
            stackLayout.Children.Remove(loaderLayout);
        }

        private async Task<List<Place>> GeneratePlaces()
        {
            // Adding loader gif
            await AddLoaderToView(MainPanel);

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

            var httpAddress = AppSettings.GetValue("PlaceRequestApi");
            return (List<Place>)request.Send(httpAddress);

        }

        private void DisableParent(View view)
        {
            var parent = view.Parent;
            var childrens = ((StackLayout)parent).Children;
            foreach (var children in childrens)
            {
                children.IsEnabled = false;
            }
        }
         
    }
}