using System;
using Xamarin.Forms;

namespace KnoWhere
{
    public partial class MainPage : ContentPage
    {
         
        public MainPage()
        {
            InitializeComponent();
            loaderLayout.Children.Add(loader);
            MainPanel.SizeChanged += MainPanel_SizeChanged;
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
             
            // Reading places from api
            places = await GeneratePlaces();

            // Creating suggestion
            CreatePlaceSuggestion(MainPanel); 
        }
         
    }
}
