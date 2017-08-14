using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Communication;
using Plugin.Geolocator;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {

        #region Properties

            private static TimeOfDay timeChosen { get; set; }
            private Enum randomPlaceType { get; set; }
            private Button button { get; set; }

        #endregion

        public MainPage()
        {
            InitializeComponent();
            CreateTimeSuggestion(MainPanel);
        }

        #region Functions
        
            private void CreatePlaceTypeSuggestion(View layout , Enum randomPlaceType)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;

                // Setting random suggestion depended on time user chose
                CreateQuestion(layout, SuggestionType.PlaceType, randomPlaceType);
                CreateAnswer(layout, SuggestionType.PlaceType, randomPlaceType);  
            }

            private void CreatePlaceSuggestion(View layout , object placeResponse)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;

                // Setting random suggestion depended on placeType user chose
                CreateQuestion(layout, SuggestionType.Place);
                CreateAnswer(layout, SuggestionType.Place);  
            }

            private void CreateTimeSuggestion(View layout)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;

                // Setting first question on every flow start
                CreateQuestion(layout, SuggestionType.Time);
                CreateAnswer(layout, SuggestionType.Time);
            }
          
            private void CreateAnswer(View layout, SuggestionType suggestionType, Enum randomPlaceType = null, object placeResponse = null)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;

                switch (suggestionType)
                {
                    case SuggestionType.Place:
                        {
                            Button nextBtn = new Button()
                            {
                                Text = "Next",
                                WidthRequest = 100
                            };

                            Button otherBtn = new Button()
                            {
                                Text = "Other",
                                WidthRequest = 50
                            };

                            Button changeTimeOfDayBtn = new Button()
                            {
                                Text = "Change time of day",
                                WidthRequest = 50
                            };

                            Button likeBtn = new Button()
                            {
                                Text = "Like",
                                WidthRequest = 50
                            };

                            List<View> buttons = new List<View>()
                            {
                                nextBtn,
                                otherBtn,
                                likeBtn,
                                changeTimeOfDayBtn
                            };

                            // Subscribe to click event
                            changeTimeOfDayBtn.Clicked += ChangeTimeOfDayBtn_Clicked;
                            nextBtn.Clicked += NextBtn_Clicked;
                            otherBtn.Clicked += OtherBtn_Clicked;
                            likeBtn.Clicked += LikeBtn_Clicked;

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
                    break;
                    case SuggestionType.PlaceType:
                        {
                            Button noBtn = new Button()
                            {
                                Text = "No",
                                WidthRequest = 100
                            };
            
                            Button yesBtn = new Button()
                            {
                                Text = "Yes",
                                WidthRequest = 50
                            };

                            List<View> buttons = new List<View>()
                            {
                                noBtn, 
                                yesBtn
                            };

                            // Subscribe to click event
                            yesBtn.Clicked += yesBtn_Clicked;
                            noBtn.Clicked += noBtn_Clicked;

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
                    break;
                    case SuggestionType.Time:
                        {
            
                            Button morning = new Button()
                            {
                                Image = "morning.png",
                                BackgroundColor = Color.Transparent,
                                BorderColor = Color.Transparent,
                                HeightRequest = 50,
                                WidthRequest = 50
                            };

                            Button afternoon = new Button()
                            {
                                Image = "afternoon.png",
                                BackgroundColor = Color.White,
                                BorderColor = Color.White,
                                HeightRequest = 50,
                                WidthRequest = 50
                            };

                            Button night = new Button()
                            {
                                Image = "night.png",
                                BackgroundColor = Color.Transparent,
                                BorderColor = Color.Transparent,
                                HeightRequest = 50,
                                WidthRequest = 50
                            };

                            List<View> dayTimes = new List<View>()
                            {
                                morning,
                                afternoon,
                                night
                            };

                            // Subscribe to click events
                            morning.Clicked += Morning_Clicked;
                            afternoon.Clicked += Afternoon_Clicked;
                            night.Clicked += Night_Clicked;

                            // Creating panel for the buttons
                            var dayTimesLayout = new StackLayout()
                            {
                                Spacing = 10,
                                Orientation = StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.End
                            };
                           
                            // Adding dayTimes images to the stackPanel
                            AddItemsToView(dayTimesLayout, dayTimes);
                            stackLayout.Children.Add(dayTimesLayout);
                        }
                    break;
                } 
            }

            private void CreateQuestion(View layout, SuggestionType suggestionType, Enum randomPlaceType = null, object placeResponse = null)
            {
                // Casting view to layout panel
                var stackLayout = layout as StackLayout;

                switch (suggestionType)
                {
                    case SuggestionType.Place:
                        {
                            
                        }
                    break;
                    case SuggestionType.PlaceType:
                        {
                            Label suggestion = new Label
                            {
                                Text = randomPlaceType.GetDisplayName(),
                                Margin = 15
                            };

                            stackLayout.Children.Add(suggestion);
                        }
                    break;
                    case SuggestionType.Time:
                        {
                            Label dayTime = new Label
                            {
                                Text = "When do you plan to go ?"
                            };

                            stackLayout.Children.Add(dayTime);
                        }
                    break;
                }
            }
         
            private Enum GenerateTimeDependedPlaces(TimeOfDay timeChosen)
            {

                // Getting placeTypes by time of the day
                Random rnd = new Random();
                int namesCount;
                int randomNumber;

                switch (timeChosen)
                {
                    case TimeOfDay.Morning:
                        {
                            namesCount = Enum.GetNames(typeof(Places.MorningPlaces)).Length;
                            randomNumber = rnd.Next(0, namesCount);
                            randomPlaceType = (Places.MorningPlaces)randomNumber;
                        }
                        break;
                    case TimeOfDay.Afternoon:
                        {
                            namesCount = Enum.GetNames(typeof(Places.AfternoonPlaces)).Length;
                            randomNumber = rnd.Next(0, namesCount);
                            randomPlaceType = (Places.AfternoonPlaces)randomNumber;
                        }
                        break;
                    case TimeOfDay.Night:
                        {
                            namesCount = Enum.GetNames(typeof(Places.NightPlaces)).Length;
                            randomNumber = rnd.Next(0, namesCount);
                            randomPlaceType = (Places.NightPlaces)randomNumber;
                        }
                        break;
                }

                return randomPlaceType;

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

            private void ChangeTimeOfDayBtn_Clicked(object sender, EventArgs e)
            {
                CreateTimeSuggestion(MainPanel); 
            }

            private void NextBtn_Clicked(object sender, EventArgs e)
            {
                
            }

            private void OtherBtn_Clicked(object sender, EventArgs e)
            {
                button = (Button)sender;
                DisableParent(button);
                randomPlaceType = GenerateTimeDependedPlaces(timeChosen);
                CreatePlaceTypeSuggestion(MainPanel, randomPlaceType);
            }

            private void LikeBtn_Clicked(object sender, EventArgs e)
            {
                
            }

            private void Night_Clicked(object sender, EventArgs e)
            { 
                button = (Button)sender;
                DisableParent(button);
                timeChosen = TimeOfDay.Night;
                randomPlaceType = GenerateTimeDependedPlaces(timeChosen);
                CreatePlaceTypeSuggestion(MainPanel, randomPlaceType);
            }

            private void Afternoon_Clicked(object sender, EventArgs e)
            {
                button = (Button)sender;
                DisableParent(button);
                timeChosen = TimeOfDay.Afternoon;
                randomPlaceType = GenerateTimeDependedPlaces(timeChosen);
                CreatePlaceTypeSuggestion(MainPanel, randomPlaceType);
            }

            private void Morning_Clicked(object sender, EventArgs e)
            {
                button = (Button)sender;
                DisableParent(button);
                timeChosen = TimeOfDay.Morning;
                randomPlaceType = GenerateTimeDependedPlaces(timeChosen);
                CreatePlaceTypeSuggestion(MainPanel, randomPlaceType);
            }

            private async void yesBtn_Clicked(object sender, EventArgs e)
            {
                button = (Button)sender;
                DisableParent(button);

                double? longitude = null;
                double? latitude = null;

                if (CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    var position = await CrossGeolocator.Current.GetPositionAsync(null, null, false);
                    longitude = position.Longitude;
                    latitude = position.Latitude;
                }

                var request = new Request(timeChosen)
                {
                    Language = Language.Hebrew,
                    Radius = 50,
                    UserLocation = !(longitude.HasValue && latitude.HasValue) ? null : 
                    new Location
                    {
                       Latitude = latitude.Value,
                       Longitude = longitude.Value
                    }
                };
            }
         
            private void noBtn_Clicked(object sender, EventArgs e)
            {
                button = (Button)sender;
                DisableParent(button);
                randomPlaceType = GenerateTimeDependedPlaces(timeChosen);
                CreatePlaceTypeSuggestion(MainPanel, randomPlaceType);
            }

        #endregion

    }
}
