using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.NET.WPF
{
    interface IElementGenerator
    {
        void AddMessage(Message message);
    }
}
