using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Emotes.Contracts
{
    public interface IEmoteFactory
    {
        Emote GetEmote(string id, string name, string url);
    }
}
