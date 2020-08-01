using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TwitchChat.NET.WPF
{
    class EmoticonTextBox : TextEditor
    {
        public ImageElementGenerator ImageElementGenerator { get; }
        public EmoticonTextBox()
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            this.ImageElementGenerator = new ImageElementGenerator();

            TextArea.TextView.ElementGenerators.Add(this.ImageElementGenerator);
        }
    }
}
