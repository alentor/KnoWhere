using System;
using System.Collections.Generic;
using System.Text;

namespace KnoWhere
{
    public struct TimeOfDay
    {

        public enum TimeOfDayText
        {
            Morning = 0,
            Afternoon = 1,
            Evening = 2,
            Night = 3
        }
       
        public static string GetTimeOfDayText(DateTime time)
        {
            string timeOfDayText;

            // Morning
            if (time.Hour >= 8 && time.Hour < 12)
                timeOfDayText = TimeOfDayText.Morning.ToString();
            // Afternoon
            else if (time.Hour >= 12 && time.Hour < 16)
                timeOfDayText = TimeOfDayText.Afternoon.ToString();
            // Evening
            else if (time.Hour >= 16 && time.Hour < 20)
                timeOfDayText = TimeOfDayText.Evening.ToString();
            // Night
            else
                timeOfDayText = TimeOfDayText.Night.ToString();
             
            return timeOfDayText;
        }
    }
}
