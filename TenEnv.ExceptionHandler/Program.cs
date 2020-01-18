using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TenEnv.ExceptionHandler
{
    public static class Program
    {
        public const string IssuesPage = "https://github.com/feel-the-dz3n/TenEnv/issues";
        public const string IssuesPageNew = "https://github.com/feel-the-dz3n/TenEnv/issues/new";

        // public const string AngryText = "Something went wrong with Base64 decoder, so let's think that this program crashed too.\r\n\r\n\r\nStupid script kiddies. >:|";
        public static string DecodedText = "";
        public static string[] Args = { };

        private static void AppendDecoded(string something)
        {
            if (DecodedText.Length != 0)
                DecodedText += Environment.NewLine + Environment.NewLine;

            DecodedText += something;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Args = args;

            if (Args.Length <= 0)
            {
                Process.Start(IssuesPage);
                Environment.Exit(0);
            }

            foreach (var arg in Args)
                AppendDecoded(arg);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(DecodedText));
        }
    }
}
