using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Communication;
using Plugin.Geolocator;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {

        private static List<Place> places { get; set; }

        private int currentPlaceIndex = 0;

        private static StackLayout loaderLayout = new StackLayout()
        {
            BackgroundColor = Color.Transparent,
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Start
        };

        private static WebView loader = new WebView
        {  
            Source = "http://thedigitalstory.com/img/hex777777_load_spinner.gif.pagespeed.ce.NtyO2jnzqo.gif",
            WidthRequest = 16,
            HeightRequest = 16,
            BackgroundColor = Color.Transparent
        };



        public MainPage()
        {
            InitializeComponent();
            loaderLayout.Children.Add(loader);
            Start(); 
        }

        public async void Start()
        {
            // Adding loader gif
            AddLoaderToView(MainPanel);

            // Init welcome msg
            Label welcomeLbl = new Label { Text = "Good " + TimeOfDay.GetTimeOfDayText(DateTime.Now) + "!" };
            
            // Removing loader gif
            RemoveLoaderFromView(MainPanel);

            // Adding welcome msg
            MainPanel.Children.Add(welcomeLbl);

            // Adding loader gif
            AddLoaderToView(MainPanel);

            // Reading places from api
            places = await GeneratePlaces();

            // Creating suggestion
            CreatePlaceSuggestion(MainPanel); 
        }

        #region Functions
        
            private async void CreatePlaceSuggestion(View layout)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;
                
                var place = places[currentPlaceIndex];

                Image image = new Image
                {
                    WidthRequest = 150,
                    HeightRequest = 150
                };

                ImageRequest imageRequest = new ImageRequest { ImageId = place.ImageId };
                
                // Retrieving the image from api
                var imageResponse = (Stream)(await imageRequest.Send());

                var imageSource = ImageSource.FromStream(() => imageResponse);
                image.Source = imageSource;

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

                // Removing loader gif
                RemoveLoaderFromView(MainPanel);

                // Adding suggestion to layout
                AddItemsToView(suggestionLayout, suggestionList);
                stackLayout.Children.Add(suggestionLayout);

                // Adding buttons to layout
                AddItemsToView(buttonsLayout, buttons);
                stackLayout.Children.Add(buttonsLayout);
            }

            private void CreatePlaceDetailsSuggestion(View layout, PlaceDetails placeDetails)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout; 

                Label suggestion = new Label
                {
                    Text = "What would you like to do ?",
                    Margin = 15
                };

                stackLayout.Children.Add(suggestion);

                Button callBtn = new Button()
                { 
                    BindingContext = placeDetails,
                    Text = "Call",
                    MinimumWidthRequest = 50
                };
            
                Button navigateBtn = new Button()
                {
                    BindingContext = placeDetails,
                    Text = "Navigate Me",
                    MinimumWidthRequest = 50
                };

                Button visitWebsiteBtn = new Button()
                {
                    BindingContext = placeDetails,
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

                // Adding buttons to the stackPanel
                AddItemsToView(buttonsLayout, buttons);
                stackLayout.Children.Add(buttonsLayout); 
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
                Thread.Sleep(500);
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

        #endregion

        #region ButtonsEvents
        
            private void CallBtn_Clicked(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                var placeDetails = button.BindingContext as PlaceDetails; 
            }
            
            private void VisitWebsiteBtn_Clicked(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                var placeDetails = button.BindingContext as PlaceDetails;

                if (placeDetails.Website != null)
                Device.OpenUri(placeDetails.Website); 
            }

            private void NavigateBtn_Clicked(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                var placeDetails = button.BindingContext as PlaceDetails; 
            }

            private void NextBtn_Clicked(object sender, EventArgs e)
            {
                currentPlaceIndex++;
                Button button = (Button)sender; 
                DisableParent(button);
                
                CreatePlaceSuggestion(MainPanel); 
            }
        
            private void interestedBtn_Clicked(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                DisableParent(button);
                
                var placeChosen = places[currentPlaceIndex];
                CreatePlaceDetailsSuggestion(MainPanel, new PlaceDetails());
            }
        
            private async Task<List<Place>> GeneratePlaces()
            {
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

                return (List<Place>)(await request.Send());
                
            }
        
        #endregion

    }
}
