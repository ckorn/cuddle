using CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Chat.Contracts
{
    public interface IMessageFormatManager
    {
        void Format(Message message);
    }
}
