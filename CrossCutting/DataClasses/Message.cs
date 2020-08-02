using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class Message
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int UsernameStartIndex { get; set; } = -1;
        public int UsernameEndIndex { get; set; } = -1;
        public Color Usercolor { get; set; }
        public string UsercolorHex { get; set; }
        public string PlainText { get; set; }
        public string DisplayMessage { get; set; }
        public int PrefixLength { get; set; }
        public List<EmotePosition> EmotePositionList { get; set; } = new List<EmotePosition>();
        public List<BadgePosition> BadgePositionList { get; set; } = new List<BadgePosition>();

        public Message()
        {

        }
    }
}
