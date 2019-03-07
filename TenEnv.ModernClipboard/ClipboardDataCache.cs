using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace TenEnv.ModernClipboard
{
    [Serializable]
    public class ClipboardDataForCache
    {
        public System.Windows.IDataObject Data => new DataObject(Format);
        public object Format;
        public DateTime Time;

        public ClipboardDataForCache(System.Windows.IDataObject data, DateTime time)
        {
            Format = data.GetData(DataFormats.Serializable);
            Time = time;
        }
    }

    [Serializable]
    public class ClipboardCache
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
            Path.Combine(Dir, "clipboard-cache.dat"));

        public List<ClipboardDataForCache> Objects = new List<ClipboardDataForCache>();

        public static ClipboardCache Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            if (!CacheFile.Exists)
                return new ClipboardCache();

            using (FileStream fs = new FileStream(CacheFile.FullName, FileMode.OpenOrCreate))
            {
                return (ClipboardCache)formatter.Deserialize(fs);
            }
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(CacheFile.FullName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
    }
}
