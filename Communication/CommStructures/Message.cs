using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class Message
    {
        public Dictionary<bool, string> SelectableOptions { get; set; }

        public string Text { get; set; }
    }
}
