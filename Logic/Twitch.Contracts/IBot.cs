using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch.Contracts
{
    public interface IBot
    {
        event EventHandler Connected;
        event EventHandler<CrossCutting.DataClasses.Message> MessageReceived;
        void Connect(string username);
        void JoinChannel(string name);
    }
}
