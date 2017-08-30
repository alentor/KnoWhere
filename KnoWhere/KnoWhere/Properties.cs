using Communication;
using System.Collections.Generic;
using Xamarin.Forms;

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

        private static PlaceDetails placeDetails { get; set; }

       
    }
}