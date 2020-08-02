using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CrossCutting.DataClasses
{
    public class Badge
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public Bitmap Bitmap { get; set; }
        public BitmapImage BitmapImage { get; set; }
    }
}
