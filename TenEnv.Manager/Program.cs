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
            
            if(!Core.Settings.Base.Tested)
            {
                bool Tested = TestTool.Test.Perform();

                Core.Settings.Base.Tested = Tested;
                Core.Settings.Base.Save();

                if (!Tested)
                {
                    var d = System.Windows.Forms.MessageBox.Show("Test failed. Do you want to continue?", "Windows 10 Environment Manager", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);

                    if (d == System.Windows.Forms.DialogResult.No)
                        Environment.Exit(1);
                }
            }

            new MainWindow().ShowDialog();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            => Core.CrashHelper.UnhandledExceptionHandle(System.Reflection.Assembly.GetCallingAssembly(), (Exception)e.ExceptionObject);
    }
}
