﻿using System;
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
        public static List<TenApp> Apps = new List<TenApp>();

        public static void InitializeAppsList()
        {
            Apps.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach (var lib in Directory.GetFiles(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "TenApp.*.dll"))
            {
                var File = new FileInfo(lib);
                Apps.Add(new TenApp(File.Name.Remove(File.Name.Length - ".dll".Length, ".dll".Length)));
            }
        }
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
        public void Initialize()
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


        /// <summary>
        /// Test TenApp
        /// </summary>
        public bool Test()
        {
            var method = LoaderType.GetMethod("TestTenApp");
            return (bool)method.Invoke(null, new object[] { });
        }
    }
}
