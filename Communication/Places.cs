using System.ComponentModel.DataAnnotations;

namespace Communication
{
    public class Places 
    {  
        public enum MorningPlaces
        { 
            [Display(Description = "Are you Hungry ?")]
            restaurant = 0,
            [Display(Description = "Are you Thirsty ?")]
            cafe = 1,
            [Display(Description = "Do you wanna go shopping ?")]
            shopping_mall = 2
        }

        public enum AfternoonPlaces
        {
            [Display(Description = "Are you Hungry ?")]
            restaurant = 0,
            [Display(Description = "Are you Thirsty ?")]
            cafe = 1,
            [Display(Description = "Do you wanna go shopping ?")]
            shopping_mall = 2
        }

        public enum NightPlaces
        {
            [Display(Description = "Are you Hungry ?")]
            restaurant = 0,
            [Display(Description = "Are you Thirsty ?")]
            bar = 1,
            [Display(Description = "Do you wanna dance ?")]
            night_club = 2,
        }
    }
}
