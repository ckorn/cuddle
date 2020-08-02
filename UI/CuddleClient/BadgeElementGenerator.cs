using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit.Rendering;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.CuddleClient
{
    class BadgeElementGenerator : VisualLineElementGenerator, IElementGenerator
    {
        private readonly EmoticonTextBox emoticonTextBox;

        public BadgeElementGenerator(EmoticonTextBox emoticonTextBox)
        {
            this.emoticonTextBox = emoticonTextBox;
        }

        private Tuple<int, BadgePosition> FindMatch(int startOffset)
        {
            var endOffset = CurrentContext.VisualLine.LastDocumentLine.EndOffset;
            //var relevantText = CurrentContext.Document.GetText(startOffset, endOffset - startOffset);
            var fullLine = CurrentContext.Document.GetText(CurrentContext.VisualLine.LastDocumentLine.Offset, endOffset - CurrentContext.VisualLine.LastDocumentLine.Offset);
            var fullLineStartOffset = startOffset - CurrentContext.VisualLine.LastDocumentLine.Offset;

            if (this.emoticonTextBox.TextMessageDict.TryGetValue(fullLine, out Message message))
            {
                foreach (BadgePosition badgePosition in message.BadgePositionList.Where(x => x.StartIndex >= fullLineStartOffset).OrderBy(x => x.StartIndex))
                {
                    return new Tuple<int, BadgePosition>(badgePosition.StartIndex - fullLineStartOffset, badgePosition);
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

            BadgePosition badgePosition = match.Item2;

            var bitmap = badgePosition?.Badge?.BitmapImage;
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

            return new InlineObjectElement(badgePosition.Text.Length, image);
        }

        #region IElementGenerator
        void IElementGenerator.AddMessage(Message message)
        {
            
        }
        #endregion
    }
}
