using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Communication;
using Plugin.Geolocator;
using System.Threading.Tasks;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {

        private static List<Place> places { get; set; }

        private int currentPlaceIndex = 0;


        public MainPage()
        {
            InitializeComponent();
            Start();
        }

        public async void Start()
        {
            places = await GeneratePlaces();
            CreatePlaceSuggestion(MainPanel);
        }

        #region Functions
        
            private void CreatePlaceSuggestion(View layout)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;
                
                var place = places[currentPlaceIndex];

                Label suggestion = new Label
                {
                    Text = "How about " + place.Name + "?",
                    Margin = 15
                };

                Image image = place.Image as Image;

                Button nextBtn = new Button()
                {
                    Text = "Next",
                    WidthRequest = 50
                };

                Button interestedBtn = new Button()
                {
                    Text = "Interested",
                    WidthRequest = 50
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

                // Adding buttons to the stackPanel
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
                    WidthRequest = 50
                };
            
                Button navigateBtn = new Button()
                {
                    BindingContext = placeDetails,
                    Text = "Navigate Me",
                    WidthRequest = 50
                };

                Button visitWebsiteBtn = new Button()
                {
                    BindingContext = placeDetails,
                    Text = "Visit Website",
                    WidthRequest = 50
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
                
                CreatePlaceSuggestion(MainPanel); 
            }
        
            private void interestedBtn_Clicked(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                DisableParent(button);
                
                var placeChosen = places[currentPlaceIndex];

                var request = new PlaceDetailsRequest
                {
                   PlaceId = placeChosen.PlaceId
                };

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
                var request = new PlaceRequest()
                {
                    Language = EnumExtensions.GetDisplayName(Language.English).ToString(),
                    Location = !(longitude.HasValue && latitude.HasValue) ? null :
                    new Location
                    {
                        Latitude = latitude.Value,
                        Longitude = longitude.Value
                    }
                };

                return null;
                
            }
        
        #endregion

    }
}
