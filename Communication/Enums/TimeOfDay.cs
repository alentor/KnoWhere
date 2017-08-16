using System.ComponentModel.DataAnnotations;

namespace Communication
{
    public enum TimeOfDay
    {
        [Display(Description = "Day/Morning")]
        Morning = 0,
        [Display(Description = "Afternoon")]
        Afternoon = 1,
        [Display(Description = "Evening/Night")]
        Night = 2
    }   
}
