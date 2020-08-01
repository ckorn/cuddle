using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class EmotePosition
    {
        public string Id { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public Emote Emote { get; set; }
    }
}
