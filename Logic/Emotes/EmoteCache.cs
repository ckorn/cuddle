using CrossCutting.DataClasses;
using Logic.Emotes.Contracts;
using Logic.HttpClient.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Emotes
{
    public class EmoteCache : IEmoteCache
    {
        private readonly Dictionary<string, Emote> emoteCache = new Dictionary<string, Emote>();
        private readonly IEmoteFactory emoteFactory;

        public EmoteCache(IEmoteFactory emoteFactory)
        {
            this.emoteFactory = emoteFactory;
        }

        public Emote GetEmote(string id, string name, string url) 
        {
            if (!emoteCache.TryGetValue(id, out Emote emote))
            {
                lock (emoteCache)
                {
                    if (!emoteCache.TryGetValue(id, out emote)) 
                    {
                        emote = emoteFactory.GetEmote(id, name, url);
                        emoteCache.Add(id, emote);
                    }
                }
            }
            return emote;
        }
    }
}
