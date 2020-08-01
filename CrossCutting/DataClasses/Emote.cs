using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CrossCutting.DataClasses
{
    public class Emote
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public Bitmap Bitmap { get; set; }
        public BitmapImage BitmapImage { get; set; }

        public Emote()
        {

        }

        public Emote(string id, string url, string name, byte[] data, Bitmap bitmap, BitmapImage bitmapImage)
        {
            Id = id;
            Url = url;
            Name = name;
            Data = data;
            Bitmap = bitmap;
            BitmapImage = bitmapImage;
        }
    }
}
