using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace TenEnv.ModernClipboard
{
    class ClipboardDataForCache
    {
        public System.Windows.IDataObject Data;
        public DateTime Time;

        public ClipboardDataForCache(System.Windows.IDataObject data, DateTime time)
        {
            Data = data;
            Time = time;
        }
    }

    class ClipboardCache
    {
        public static string Dir
        {
            get
            {
                var dir = new DirectoryInfo(
                    System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "TenEnv", "Cache"));

                if (!dir.Exists)
                    dir.Create();

                return dir.FullName;
            }
        }

        public static FileInfo CacheFile => new FileInfo(
            Path.Combine(Dir, "cache.dat"));

        public List<ClipboardDataForCache> Objects = new List<ClipboardDataForCache>();
    }
}
