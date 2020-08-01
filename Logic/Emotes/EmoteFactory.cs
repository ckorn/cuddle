using CrossCutting.DataClasses;
using Logic.Emotes.Contracts;
using Logic.HttpClient.Contracts;
using Logic.ImageManagement.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Emotes
{
    public class EmoteFactory : IEmoteFactory
    {
        private readonly IHttpClient httpClient;
        private readonly IBitmapFunctions bitmapFunctions;

        public EmoteFactory(IHttpClient httpClient, IBitmapFunctions bitmapFunctions)
        {
            this.httpClient = httpClient;
            this.bitmapFunctions = bitmapFunctions;
        }

        public Emote GetEmote(string id, string name, string url) 
        {
            Emote emote = new Emote() { Id = id, Name = name, Url = url, Data = this.httpClient.DownloadData(url) };
            using (Stream stream = new MemoryStream(emote.Data))
            {
                Bitmap bitmap = new Bitmap(stream);
                emote.Bitmap = bitmap;
                emote.BitmapImage = this.bitmapFunctions.ToBitmapImage(emote.Bitmap);
            }
            return emote;
        }
    }
}
