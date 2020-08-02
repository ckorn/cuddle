using CrossCutting.DataClasses;
using CrossCutting.Logging.Contracts;
using Logic.Badges.Contracts;
using Logic.HttpClient.Contracts;
using Logic.ImageManagement.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Badges
{
    public class BadgeFactory : IBadgeFactory
    {
        private readonly IHttpClient httpClient;
        private readonly IBitmapFunctions bitmapFunctions;
        private readonly ILogger logger;

        public BadgeFactory(IHttpClient httpClient, IBitmapFunctions bitmapFunctions, ILogger logger)
        {
            this.httpClient = httpClient;
            this.bitmapFunctions = bitmapFunctions;
            this.logger = logger;
        }

        public Badge GetBadge(string id, string name, string url)
        {
            try
            {
                Badge badge = new Badge() { Id = id, Name = name, Url = url, Data = this.httpClient.DownloadData(url) };
                using (Stream stream = new MemoryStream(badge.Data))
                {
                    Bitmap bitmap = new Bitmap(stream);
                    badge.Bitmap = bitmap;
                    badge.BitmapImage = this.bitmapFunctions.ToBitmapImage(badge.Bitmap);
                }
                return badge;
            }
            catch (Exception e)
            {
                this.logger.Log($"Error: Creating bitmap for {id}/{name}({url}): {e.Message}");
                return null;
            }
        }
    }
}
