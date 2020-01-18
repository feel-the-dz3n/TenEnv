using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenAppSpace
{
    public class TenAppLoader
    {
        public static TenEnv.Core.GlobalKeyboardHook Hook;

        public static void StopTenApp()
        {
            TenApp.DWMTools.DWMScreenshotTool.Stop();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void InitializeTenApp()
        {
            Hook = new TenEnv.Core.GlobalKeyboardHook();

            TenApp.DWMTools.DWMScreenshotTool.Initialize();
        }
    }
}
