using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit.Rendering;
using Logic.Chat;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.NET.WPF
{
    class ImageElementGenerator : VisualLineElementGenerator, IMessageFormatManager
    {
        // To use this class:
        // textEditor.TextArea.TextView.ElementGenerators.Add(new ImageElementGenerator());

        private static readonly Dictionary<string, Message> textMessageDict = new Dictionary<string, Message>();

        private readonly Dictionary<string, Emote> emoteDict;
        private readonly MessageFormatManager messageFormatManager = new MessageFormatManager();

        public ImageElementGenerator()
        {
            emoteDict = new Dictionary<string, Emote>();
        }

        private Tuple<int, string> FindMatch(int startOffset)
        {
            var endOffset = CurrentContext.VisualLine.LastDocumentLine.EndOffset;
            //var relevantText = CurrentContext.Document.GetText(startOffset, endOffset - startOffset);
            var fullLine = CurrentContext.Document.GetText(CurrentContext.VisualLine.LastDocumentLine.Offset, endOffset - CurrentContext.VisualLine.LastDocumentLine.Offset);
            var fullLineStartOffset = startOffset - CurrentContext.VisualLine.LastDocumentLine.Offset;

            if (textMessageDict.TryGetValue(fullLine, out Message message)) 
            {
                int indexCompare = (fullLineStartOffset == 0) ? 0 : fullLineStartOffset - message.PrefixLength;
                foreach (EmotePosition emotePosition in message.EmotePositionList.Where(x => x.StartIndex >= indexCompare).OrderBy(x => x.StartIndex))
                {
                    return new Tuple<int, string>(emotePosition.StartIndex - fullLineStartOffset + message.PrefixLength, emotePosition.Emote.Name);
                }
            }

            return null;
        }

        public override int GetFirstInterestedOffset(int startOffset)
        {
            var match = FindMatch(startOffset);
            return (match != null) ? startOffset + match.Item1 : -1;
        }

        public override VisualLineElement ConstructElement(int offset)
        {
            var match = FindMatch(offset);
            if (match == null || match.Item1 != 0)
            {
                return null;
            }

            var key = match.Item2;
            emoteDict.TryGetValue(key, out Emote emoticon);

            var bitmap = emoticon?.BitmapImage;
            if (bitmap == null)
            {
                return null;
            }

            var image = new System.Windows.Controls.Image
            {
                Source = bitmap,
                Width = bitmap.PixelWidth,
                Height = bitmap.PixelHeight
            };

            return new InlineObjectElement(match.Item2.Length, image);
        }

        #region IMessageFormatManager
        void IMessageFormatManager.Format(Message message)
        {
            this.messageFormatManager.Format(message);
            if (!textMessageDict.ContainsKey(message.DisplayMessage))
            {
                textMessageDict.Add(message.DisplayMessage, message);
                foreach (EmotePosition emotePosition in message.EmotePositionList)
                {
                    if ((!this.emoteDict.ContainsKey(emotePosition.Emote.Name)) && (!string.IsNullOrEmpty(emotePosition?.Emote?.Name)))
                    {
                        this.emoteDict.Add(emotePosition.Emote.Name, emotePosition.Emote);
                    }
                }
            }
        } 
        #endregion
    }
}
