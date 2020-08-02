using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Badges.Contracts
{
    public interface IBadgeCache
    {
        void AddBadge(string id, string name, string url);
        Badge GetBadge(string id);
    }
}
