using DotNetXmlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenEnv.Core
{
    [Serializable]
    public class BaseSettings : XmlConfig
    {
        public bool Tested = false;
        public System.Windows.Media.Color MainColor = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);

        public ClipboardConfig Clipboard = new ClipboardConfig();

        public BaseSettings() { }
    }

    [Serializable]
    public class ClipboardConfig
    {
        public int MaximumEntries = 10;
        public bool IgnoreFiles = false;
    }
}
