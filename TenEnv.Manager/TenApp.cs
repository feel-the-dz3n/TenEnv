using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TenEnv.Manager
{
    public class TenApps
    {
        public static List<TenApp> Apps = new List<TenApp>()
        {
            new TenApp("TenApp.ModernClipboard"),
            new TenApp("TenApp.ModernScreenshot")
        };
    }

    public class TenApp
    {
        public FileInfo File { get; private set; }
        public string ProcessName { get; private set; }
        public Process CurrentProcess => GetProcess();

        public Assembly Info => Assembly.LoadFile(File.FullName);

        public AssemblyDescriptionAttribute Description =>
            Info.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault();
        public AssemblyTitleAttribute Title =>
            Info.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).OfType<AssemblyTitleAttribute>().FirstOrDefault();


        private string StartArguments
            => $"-man:" + Process.GetCurrentProcess().Id;

        public TenApp(string exe)
        {
            if (!exe.EndsWith(".exe"))
                exe += ".exe";

            File = new FileInfo(exe);
            ProcessName = File.Name.Remove(File.Name.Length - ".exe".Length, ".exe".Length); // genius
        }

        public Process GetProcess()
        {
            var p = Process.GetProcessesByName(ProcessName);

            if (p.Length == 0)
                return null;
            else
                return p[0];
        }

        /// <summary>
        /// Runs or restart TenApp
        /// </summary>
        public void Restart() => Run();

        /// <summary>
        /// Runs or restarts TenApp
        /// </summary>
        public void Run()
        {
            var p = GetProcess();

            // If the process found, kill it
            if (p != null)
                Stop(true);

            // Create new process and start it
            p = new Process();
            p.StartInfo.FileName = File.FullName;
            p.StartInfo.Arguments = StartArguments;
            p.Start();
        }

        /// <summary>
        /// Kills TenApp
        /// </summary>
        public void Stop(bool WaitForExit = false)
        {
            var p = GetProcess();

            if (p != null)
            {
                p.Kill();

                if (WaitForExit)
                    p.WaitForExit();
            }
        }
    }
}
