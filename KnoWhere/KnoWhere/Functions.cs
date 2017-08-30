using Communication;
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
            AddLoaderToView(MainPanel);

            // Casting view to layout panel
            var stackLayout = layout as StackLayout;

            var place = places[currentPlaceIndex];

            Image image = new Image
            {
                WidthRequest = 150,
                HeightRequest = 150
            };

            // Retrieving the image from api
            if (!String.IsNullOrEmpty(place.ImageId))
            {
                ImageRequest imageRequest = new ImageRequest { ImageId = place.ImageId };
                var imageResponse = (Stream)(await imageRequest.SendAsync());
                var imageSource = ImageSource.FromStream(() => imageResponse);
                image.Source = imageSource;
            }

            Label suggestion = new Label
            {
                Text = "How about " + place.Name + "?",
                Margin = 15
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
                MinimumWidthRequest = 50
            };

            Button interestedBtn = new Button()
            {
                Text = "Interested",
                MinimumWidthRequest = 50
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
                Padding = 15,
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
            AddLoaderToView(MainPanel);

            // Casting view to layout panel
            var stackLayout = layout as StackLayout;

            Label suggestion = new Label
            {
                Text = "What would you like to do ?",
                Margin = 15
            };

            Button callBtn = new Button()
            {
                Text = "Call",
                MinimumWidthRequest = 50
            };

            Button navigateBtn = new Button()
            {
                Text = "Navigate Me",
                MinimumWidthRequest = 50
            };

            Button visitWebsiteBtn = new Button()
            {
                Text = "Visit Website",
                MinimumWidthRequest = 50
            };

            List<View> buttons = new List<View>()
            {
                callBtn,
                navigateBtn,
                visitWebsiteBtn
            };

            // Subscribe to click event
            callBtn.Clicked += CallBtn_Clicked;
            navigateBtn.Clicked += NavigateBtn_Clicked;
            visitWebsiteBtn.Clicked += VisitWebsiteBtn_Clicked;

            // Creating panel for the buttons
            var buttonsLayout = new StackLayout()
            {
                Padding = 15,
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
            await Task.Run(() => 
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Casting view to layout panel
                    MainPanel.Children.Add(loaderLayout);
                });

                // Holding background thread for specified milliseconds
                Thread.Sleep(10000);
            });

            // Removing the loader gif
            RemoveLoaderFromView(MainPanel);

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

        private void AddLoaderToView(View layout)
        {
            // Casting view to layout panel
            var stackLayout = layout as StackLayout;
            stackLayout.Children.Add(loaderLayout);
        }

        private void RemoveLoaderFromView(View layout)
        {
            // Casting view to layout panel
            var stackLayout = layout as StackLayout;
            stackLayout.Children.Remove(loaderLayout);
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