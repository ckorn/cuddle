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
        public EmoticonTextBox()
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

            TextArea.TextView.ElementGenerators.Add(new ImageElementGenerator());
        }
    }
}
