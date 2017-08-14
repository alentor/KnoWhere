using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class Request
    { 
        private DateTime TimeFrom { get; set; }

        private DateTime TimeTo { get; set; }

        public Location UserLocation { get; set; }

        public int Radius { get; set; }

        private string language { get; set; }

        public Language Language
        {
            get { return (Language)Enum.Parse(typeof(Language), language); }
            set { language = value.GetDisplayName(); }
        }
        
        public Request(TimeOfDay timeChosen)
        {
            TimeFrom = DateTime.Now;
            TimeTo = DateTime.Now;
            TimeSpan fromHour;
            TimeSpan toHour;

            switch (timeChosen)
            {
                case TimeOfDay.Morning:
                    {
                        fromHour = new TimeSpan(08, 00, 00);
                        toHour = new TimeSpan(12, 00, 00);
                        TimeFrom = TimeFrom.Date + fromHour;
                        TimeTo = TimeTo.Date + toHour;
                    }
                    break;
                case TimeOfDay.Afternoon:
                    {
                        fromHour = new TimeSpan(12, 00, 00);
                        toHour = new TimeSpan(19, 00, 00);
                        TimeFrom = TimeFrom.Date + fromHour;
                        TimeTo = TimeTo.Date + toHour;
                    }
                    break;
                case TimeOfDay.Night:
                    {
                        fromHour = new TimeSpan(19, 00, 00);
                        toHour = new TimeSpan(01, 00, 00);
                        TimeFrom = TimeFrom.Date + fromHour;
                        TimeTo = TimeTo.Date + toHour;
                    }
                    break;
            }
        }
    }
}
