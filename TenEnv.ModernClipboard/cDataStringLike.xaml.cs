using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TenEnv.ModernClipboard
{
    /// <summary>
    /// Interaction logic for cDataStringLike.xaml
    /// </summary>
    public partial class cDataStringLike : UserControl
    {
        public cDataStringLike(string text, string format = "")
        {
            InitializeComponent();
            tb.Text = ClearString(Core.Core.TrimString(text, 300));
            tbFormat.Text = format;
        }



        private char[] BadSymbols = { '\r', '\n', ' ', '\t' };

        private string ClearString(string source)
        {
            var prev = source;

            source = source.TrimStart(BadSymbols);

            if (source != prev)
                source = "..." + source;

            return source;
        }
    }
}
