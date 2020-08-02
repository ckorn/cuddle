using CrossCutting.DataClasses;
using Logic.Badges.Contracts;
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
        private readonly IBadgeCache badgeCache;
        public MessageConverter(IEmoteCache emoteCache, IBadgeCache badgeCache)
        {
            this.emoteCache = emoteCache;
            this.badgeCache = badgeCache;
        }

        public Message ConvertMessage(ChatMessage message) 
        {
            Message ret = new Message()
            {
                Id = message.Id,
                PlainText = message.Message,
                Username = message.Username,
                Usercolor = message.Color,
            };
            foreach (TwitchLib.Client.Models.Emote item in message.EmoteSet.Emotes)
            {
                CrossCutting.DataClasses.Emote emote = this.emoteCache.GetEmote(item.Id, item.Name, item.ImageUrl);
                ret.EmotePositionList.Add(new EmotePosition() { Emote = emote, EndIndex = item.EndIndex, StartIndex = item.StartIndex, Id = item.Id });
            }
            foreach (KeyValuePair<string, string> badgeVersion in message.Badges)
            {
                Badge badge = this.badgeCache.GetBadge(badgeVersion.Key);
                badge = badge ?? this.badgeCache.GetBadge($"{badgeVersion.Key}_{badgeVersion.Value}");
                badge = badge ?? this.badgeCache.GetBadge($"{message.Channel}_{badgeVersion.Key}");
                badge = badge ?? this.badgeCache.GetBadge($"{message.Channel}_{badgeVersion.Key}_{badgeVersion.Value}");
                ret.BadgePositionList.Add(new BadgePosition($"{message.Channel}_{badgeVersion.Key}_{badgeVersion.Value}", -1, -1, $"badge:{badgeVersion.Key}_{badgeVersion.Value}", badge));
            }
            return ret;
        }
    }
}
