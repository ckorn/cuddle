using Logic.Badges.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.V5.Models.Badges;

namespace Logic.Twitch
{
    internal class BadgeConverter
    {
        private readonly IBadgeCache badgeCache;
        public BadgeConverter(IBadgeCache badgeCache)
        {
            this.badgeCache = badgeCache;
        }

        public void ConvertGlobalBadges(GlobalBadgesResponse globalBadgesResponse) 
        {
            if (globalBadgesResponse == null)
            {
                return;
            }
            foreach (KeyValuePair<string, Badge> idBadge in globalBadgesResponse.Sets)
            {
                foreach (KeyValuePair<string, BadgeContent> versionBadgeContent in idBadge.Value.Versions)
                {
                    this.badgeCache.AddBadge($"{idBadge.Key}_{versionBadgeContent.Key}", versionBadgeContent.Value.Title, versionBadgeContent.Value.Image_Url_1x);
                }
            }
        }

        public void ConvertChannelBadges(string channel, ChannelDisplayBadges channelDisplayBadges) 
        {
            if (channelDisplayBadges == null)
            {
                return;
            }

            if (channelDisplayBadges?.Sets?.Subscriber?.Versions != null)
            {
                foreach (KeyValuePair<string, BadgeContent> versionBadgeContent in channelDisplayBadges.Sets.Subscriber.Versions)
                {
                    this.badgeCache.AddBadge($"{channel}_subscriber_{versionBadgeContent.Key}", versionBadgeContent.Value.Title, versionBadgeContent.Value.Image_Url_1x);
                }
            }
            if (channelDisplayBadges?.Sets?.Bits?.Versions != null)
            {
                foreach (KeyValuePair<string, BadgeContent> versionBadgeContent in channelDisplayBadges.Sets.Bits.Versions)
                {
                    this.badgeCache.AddBadge($"{channel}_bits_{versionBadgeContent.Key}", versionBadgeContent.Value.Title, versionBadgeContent.Value.Image_Url_1x);
                }
            }
        }
    }
}
