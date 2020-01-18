using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TenEnv.Core
{
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
    }
}
