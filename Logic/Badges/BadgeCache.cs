using CrossCutting.DataClasses;
using Logic.Badges.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Badges
{
    public class BadgeCache : IBadgeCache
    {
        private readonly Dictionary<string, Badge> badgeCache = new Dictionary<string, Badge>();
        private readonly IBadgeFactory badgeFactory;

        public BadgeCache(IBadgeFactory badgeFactory)
        {
            this.badgeFactory = badgeFactory;
        }

        public void AddBadge(string id, string name, string url)
        {
            if (!badgeCache.TryGetValue(id, out Badge badge))
            {
                lock (badgeCache)
                {
                    if (!badgeCache.TryGetValue(id, out badge))
                    {
                        badge = badgeFactory.GetBadge(id, name, url);
                        if (badge != null)
                        {
                            badgeCache.Add(id, badge);
                        }
                    }
                }
            }
        }

        public Badge GetBadge(string id)
        {
            if (badgeCache.ContainsKey(id))
            {
                return badgeCache[id];
            }
            return null;
        }
    }
}
