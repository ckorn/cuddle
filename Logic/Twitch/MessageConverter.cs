using Logic.Emotes.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace Logic.Twitch
{
    internal class MessageConverter
    {
        private readonly IEmoteCache emoteCache;
        public MessageConverter(IEmoteCache emoteCache)
        {
            this.emoteCache = emoteCache;
        }

        public CrossCutting.DataClasses.Message ConvertMessage(ChatMessage message) 
        {
            CrossCutting.DataClasses.Message ret = new CrossCutting.DataClasses.Message() { PlainText = message.Message };
            foreach (Emote item in message.EmoteSet.Emotes)
            {
                CrossCutting.DataClasses.Emote emote = this.emoteCache.GetEmote(item.Id, item.Name, item.ImageUrl);
                ret.EmotePositionList.Add(new CrossCutting.DataClasses.EmotePosition() { Emote = emote, EndIndex = item.EndIndex, StartIndex = item.StartIndex, Id = item.Id });
            }
            return ret;
        }
    }
}
