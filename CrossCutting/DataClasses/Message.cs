using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class Message
    {
        public string PlainText { get; set; }
        public List<EmotePosition> EmotePositionList { get; set; } = new List<EmotePosition>();
        public string EmoteText { get; set; }
    }
}
