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
        public Assembly Assembly { get; private set; }
        public Type LoaderType { get; private set; }

        public AssemblyDescriptionAttribute Description =>
            Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault();
        public AssemblyTitleAttribute Title =>
            Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).OfType<AssemblyTitleAttribute>().FirstOrDefault();

        public TenApp(string dll)
        {
            if (!dll.EndsWith(".dll"))
                dll += ".dll";

            File = new FileInfo(dll);
            Assembly = Assembly.LoadFile(File.FullName);
            LoaderType = Assembly.GetType("TenAppSpace.TenAppLoader");
        }

        /// <summary>
        /// Runs TenApp
        /// </summary>
        public void CreateInstance()
        {
            var method = LoaderType.GetMethod("InitializeTenApp");
            method.Invoke(null, new object[] { });
        }

        /// <summary>
        /// Stop instance
        /// </summary>
        public void Stop()
        {
            var method = LoaderType.GetMethod("StopTenApp");
            method.Invoke(null, new object[] { });
        }
    }
}
