using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenEnv.Core
{
    public class Core
    {
        public static System.Windows.Input.Key SuperKey
        {
            get
            {
                if (IsWin10)
                    return System.Windows.Input.Key.LeftAlt;
                else
                    return System.Windows.Input.Key.LWin;
            }
        }

        public static bool IsWin10
        {
            get
            {
                return true; // fix me
            }
        }

        public static void DisplayWarnings()
        {
        }
    }
}
