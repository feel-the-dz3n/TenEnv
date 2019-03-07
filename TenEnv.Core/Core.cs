using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace TenEnv.Core
{
    public class Core
    {
        public static System.Windows.Input.Key SuperKey
        {
            get
            {
                if (IsWin10)
                    return System.Windows.Input.Key.LeftAlt;
                else
                    return System.Windows.Input.Key.LWin;
            }
        }

        public static bool IsWin10
        {
            get
            {
                return true; // fix me
            }
        }

        public static BitmapImage ImageSourceFromBitmap(System.Drawing.Bitmap bmp)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static string TrimString(string source, int newCount)
        {
            string result = "";

            for (int i = 0; i < source.Length; i++)
            {
                if (i == newCount - 1)
                    break;

                result += source[i];
            }

            return result;
        }

        public static void DisplayWarnings()
        {
        }
    }
}
