using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TenEnv.Core;

namespace TenAppSpace
{
    public class TenAppLoader
    {
        private static TenEnv.ModernClipboard.ClipboardWnd MainWnd;

        public static void StopTenApp()
        {
            if (MainWnd == null)
                return;

            MainWnd.Close();
            MainWnd = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void InitializeTenApp()
        {
            StopTenApp();

            MainWnd = new TenEnv.ModernClipboard.ClipboardWnd();
            MainWnd.ShowDialog();
        }
    }
}
