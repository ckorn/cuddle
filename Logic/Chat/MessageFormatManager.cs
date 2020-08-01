using CrossCutting.DataClasses;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Chat
{
    public class MessageFormatManager : IMessageFormatManager
    {
        public void Format(Message message)
        {
            string prefix = $"{message.Username}: ";
            message.DisplayMessage = prefix + message.PlainText;
            message.PrefixLength = prefix.Length;
        }
    }
}
