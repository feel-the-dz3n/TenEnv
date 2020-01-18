using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenEnv.Manager
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TenApps.InitializeAppsList();

            new MainWindow().ShowDialog();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            => Core.CrashHelper.UnhandledExceptionHandle(System.Reflection.Assembly.GetCallingAssembly(), (Exception)e.ExceptionObject);
    }
}
