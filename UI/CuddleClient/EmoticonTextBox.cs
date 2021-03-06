﻿using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit;
using Logic.Chat;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.CuddleClient
{
    class EmoticonTextBox : TextEditor
    {
        public Dictionary<string, Message> TextMessageDict { get; } = new Dictionary<string, Message>();
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

        public void AddMessage(Message message)
        {
            if (!TextMessageDict.ContainsKey(message.DisplayMessage))
            {
                TextMessageDict.Add(message.DisplayMessage, message);
                foreach (IElementGenerator elementGenerator in elementGeneratorList)
                {
                    elementGenerator.AddMessage(message);
                }
            }
        }
    }
}
