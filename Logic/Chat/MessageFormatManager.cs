using CrossCutting.DataClasses;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Chat
{
    internal class MessageFormatManager : IMessageFormatManager
    {
        public void Format(Message message)
        {
            StringBuilder prefixBuilder = new StringBuilder();
            int pos = 0;
            foreach (BadgePosition badgePosition in message.BadgePositionList)
            {
                badgePosition.StartIndex = pos;
                badgePosition.EndIndex = pos + badgePosition.Text.Length;
                prefixBuilder.Append(badgePosition.Text);
                prefixBuilder.Append(" ");
                pos += badgePosition.Text.Length + 1;
            }
            prefixBuilder.Append($"{message.Username}: ");
            message.UsernameStartIndex = pos;
            message.UsernameEndIndex = pos + message.Username.Length;

            string prefix = prefixBuilder.ToString();
            message.DisplayMessage = prefix + message.PlainText;
            message.PrefixLength = prefix.Length;
        }
    }
}
