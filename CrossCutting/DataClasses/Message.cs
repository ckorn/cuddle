using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class Message
    {
        public string Username { get; set; }
        public string PlainText { get; set; }
        public string DisplayMessage { get; set; }
        public int PrefixLength { get; set; }
        public List<EmotePosition> EmotePositionList { get; set; } = new List<EmotePosition>();

        public Message()
        {

        }
    }
}
