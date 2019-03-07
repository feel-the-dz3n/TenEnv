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

            if (args.Contains("/reset"))
                Core.XmlConfig.Delete();

            var cfg = Core.XmlConfig.LoadConfig();

            if(!cfg.Tested)
            {
                bool Tested = TestTool.Test.Perform();

                cfg.Tested = Tested;
                cfg.SaveSettings();

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
