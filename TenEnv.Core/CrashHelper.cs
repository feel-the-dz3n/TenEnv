using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TenEnv.Core
{
    public class CrashHelper
    {
        private const string InitialText = "Something of Windows 10 Environment just crashed. Please, report this here:\r\n" +
            "https://github.com/feel-the-dz3n/TenEnv/issues/new" +
            "\r\n\r\n";
        private const string CrashHandlerExe = "TenEnv.ExceptionHandler.exe";

        public static void UnhandledExceptionHandle(Assembly assembly, Exception exception)
        {
            if (Debugger.IsAttached)
                return;

            StringBuilder a = new StringBuilder();
            a.Append(InitialText);
            a.AppendLine("Assembly: " + assembly.FullName);
            a.AppendLine("Time: " + DateTime.Now);
            a.AppendLine("OS: " + Environment.OSVersion.VersionString);
            a.AppendLine("Used Memory: ~" + Math.Round(Process.GetCurrentProcess().PrivateMemorySize64 / 1e+6, 2) + " mb");
            a.AppendLine();
            a.AppendLine("Exception Details:");
            a.Append(exception.ToString());

            try
            {
                if (File.Exists(CrashHandlerExe))
                {
                    string args = "\"base64:" + Base64Encode(a.ToString()) + "\"";

                    Process.Start(CrashHandlerExe, args);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show(
                    "Press Ctrl+C to copy this\r\n\r\n" + a.ToString(), 
                    "Windows 10 Environment Crashed", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

            Environment.Exit(1);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
