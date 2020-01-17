using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

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

namespace TenEnv.ModernClipboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Core.ManAttach Man;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            string[] args = e.Args;

            var manProcess = Core.TenAppHelper.GetManagerProcessFromArguments(args);
            Man = new Core.ManAttach(System.Diagnostics.Process.GetCurrentProcess(), manProcess);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            => Core.CrashHelper.UnhandledExceptionHandle(System.Reflection.Assembly.GetCallingAssembly(), (Exception)e.ExceptionObject);
    }
}
