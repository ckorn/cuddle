using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class BadgePosition
    {
        public string Id { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Text { get; set; }
        public Badge Badge { get; set; }

        public BadgePosition()
        {

        }

        public BadgePosition(string id, int startIndex, int endIndex, string text, Badge badge)
        {
            Id = id;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Text = text;
            Badge = badge;
        }
    }
}
