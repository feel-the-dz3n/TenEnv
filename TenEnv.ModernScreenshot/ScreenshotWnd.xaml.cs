using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static TenEnv.Core.Extensions;

namespace TenEnv.ModernScreenshot
{
    /// <summary>
    /// Interaction logic for ScreenshotWnd.xaml
    /// </summary>
    public partial class ScreenshotWnd : Window
    {
        public const int FadeTime = 500;
        Core.GlobalKeyboardHook Kbd = null;

        public ScreenshotWnd()
        {
            InitializeComponent();

            cButtons.MainWnd = this;

            MainGrid.Opacity = 0;

            Kbd = new Core.GlobalKeyboardHook();
            Kbd.KeyboardPressed += Kbd_KeyboardPressed;
        }

        private void Kbd_KeyboardPressed(object sender, Core.GlobalKeyboardHookEventArgs e)
        {
            var s = e.KeyboardState;
            if (s == Core.GlobalKeyboardHook.KeyboardState.SysKeyDown &&
                e.Key == Key.S &&
                Core.Keyboard.IsKeyDown(Key.LeftShift) &&
                Core.Keyboard.IsKeyDown(Core.Core.SuperKey))
            {
                e.Handled = true;
                DoSnip();
            }
        }

        public static BitmapImage GetScreenshot()
        {
            double screenLeft = SystemParameters.VirtualScreenLeft;
            double screenTop = SystemParameters.VirtualScreenTop;
            double screenWidth = SystemParameters.VirtualScreenWidth;
            double screenHeight = SystemParameters.VirtualScreenHeight;

            using (Bitmap bmp = new Bitmap((int)screenWidth,
                (int)screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);

                    return Core.Core.ImageSourceFromBitmap(bmp);
                }

            }
        }

        public void Finish(ImageSource img)
        {
            HideSnip();

            BitmapImage i = (BitmapImage)img;
            Clipboard.SetImage(i);
        }

        public void Clear()
        {
            imgScreen.Source = null;
        }

        public void HideSnip()
        {
            MainGrid.Opacity = 0;
            this.IsHitTestVisible = false;
            Clear();
            this.Visibility = Visibility.Hidden;
        }

        public void DoSnip()
        {
            if(this.Visibility == Visibility.Visible)
            {
                HideSnip();
                return;
            }

            this.Visibility = Visibility.Visible;

            this.IsHitTestVisible = true;
            // MainGrid.Opacity = 1;
            MainGrid.FadeIn(FadeTime, 250);
            this.Activate();

            throw new Exception("Fuck");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            HideSnip();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }
    }
}
