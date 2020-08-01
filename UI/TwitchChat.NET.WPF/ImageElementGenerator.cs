using CrossCutting.DataClasses;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitchChat.NET.WPF
{
    class ImageElementGenerator : VisualLineElementGenerator
    {
        // To use this class:
        // textEditor.TextArea.TextView.ElementGenerators.Add(new ImageElementGenerator());

        private static readonly Regex ImageRegex = new Regex(@":D", RegexOptions.IgnoreCase);

        private readonly Dictionary<string, Emote> emoteDict;

        public ImageElementGenerator()
        {
            emoteDict = new Dictionary<string, Emote>();
        }

        private Match FindMatch(int startOffset)
        {
            var endOffset = CurrentContext.VisualLine.LastDocumentLine.EndOffset;
            var relevantText = CurrentContext.Document.GetText(startOffset, endOffset - startOffset);

            return ImageRegex.Match(relevantText);
        }

        public override int GetFirstInterestedOffset(int startOffset)
        {
            var match = FindMatch(startOffset);
            return match.Success ? startOffset + match.Index : -1;
        }

        public override VisualLineElement ConstructElement(int offset)
        {
            var match = FindMatch(offset);
            if (!match.Success || match.Index != 0)
                return null;

            var key = match.Groups[0].Value;
            emoteDict.TryGetValue(key, out Emote emoticon);

            var bitmap = emoticon?.BitmapImage;
            if (bitmap == null)
                return null;

            var image = new System.Windows.Controls.Image
            {
                Source = bitmap,
                Width = bitmap.PixelWidth,
                Height = bitmap.PixelHeight
            };

            return new InlineObjectElement(match.Length, image);
        }
    }
}
