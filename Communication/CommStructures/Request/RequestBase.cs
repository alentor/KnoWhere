using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public abstract class RequestBase
    {
        private string language { get; set; }

        public Language Language
        {
            get { return (Language)Enum.Parse(typeof(Language), language); }
            set { language = value.GetDisplayName(); }
        }
 
        public RequestType Type { get; set; }
        
        public Guid UserId { get; set; }
    }
}
