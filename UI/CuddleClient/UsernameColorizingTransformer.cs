using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UI.CuddleClient
{
    class UsernameColorizingTransformer : DocumentColorizingTransformer
    {
        private readonly EmoticonTextBox emoticonTextBox;

        public UsernameColorizingTransformer(EmoticonTextBox emoticonTextBox)
        {
            this.emoticonTextBox = emoticonTextBox;
        }

        protected override void ColorizeLine(DocumentLine line)
        {
            if (!line.IsDeleted)
            {
                string fullLine = CurrentContext.Document.GetText(line.Offset, line.EndOffset - line.Offset);
                if (this.emoticonTextBox.TextMessageDict.TryGetValue(fullLine, out Message message))
                {
                    ChangeLinePart(line.Offset + message.UsernameStartIndex, line.Offset + message.UsernameEndIndex, (x) => ApplyChanges(x, message));
                }
            }
        }

        void ApplyChanges(VisualLineElement element, Message message)
        {
            if (!string.IsNullOrEmpty(message.UsercolorHex))
            {
                Color color = (Color)ColorConverter.ConvertFromString(message.UsercolorHex);
                element.TextRunProperties.SetForegroundBrush(new SolidColorBrush(color));
            }
            Typeface typeface = element.TextRunProperties.Typeface;
            element.TextRunProperties.SetTypeface(new Typeface(typeface.FontFamily, typeface.Style, FontWeights.Bold, typeface.Stretch));
        }
    }
}
