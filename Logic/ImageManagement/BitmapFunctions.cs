using Logic.ImageManagement.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Logic.ImageManagement
{
    public class BitmapFunctions : IBitmapFunctions
    {
        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;

                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.DecodePixelHeight = 18;
                image.DecodePixelWidth = 18;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();

                return image;
            }
        }
    }
}
