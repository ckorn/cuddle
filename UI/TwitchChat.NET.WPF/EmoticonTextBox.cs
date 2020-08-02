using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit;
using Logic.Chat;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TwitchChat.NET.WPF
{
    class EmoticonTextBox : TextEditor, IMessageFormatManager
    {
        public Dictionary<string, Message> TextMessageDict { get; } = new Dictionary<string, Message>();
        private readonly MessageFormatManager messageFormatManager = new MessageFormatManager();
        private readonly List<IElementGenerator> elementGeneratorList = new List<IElementGenerator>();

        public EmoticonTextBox()
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            EmoteElementGenerator emoteElementGenerator = new EmoteElementGenerator(this);
            BadgeElementGenerator badgeElementGenerator = new BadgeElementGenerator(this);
            UsernameColorizingTransformer usernameColorizingTransformer = new UsernameColorizingTransformer(this);

            TextArea.TextView.ElementGenerators.Add(emoteElementGenerator);
            TextArea.TextView.ElementGenerators.Add(badgeElementGenerator);
            TextArea.TextView.LineTransformers.Add(usernameColorizingTransformer);
            elementGeneratorList.Add(emoteElementGenerator);
            elementGeneratorList.Add(badgeElementGenerator);
        }

        #region IMessageFormatManager
        void IMessageFormatManager.Format(Message message)
        {
            this.messageFormatManager.Format(message);
            if (!TextMessageDict.ContainsKey(message.DisplayMessage))
            {
                TextMessageDict.Add(message.DisplayMessage, message);
                foreach (IElementGenerator elementGenerator in elementGeneratorList)
                {
                    elementGenerator.AddMessage(message);
                }
            }
        }
        #endregion
    }
}
