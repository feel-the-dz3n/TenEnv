using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TenEnv.ModernClipboard
{
    /// <summary>
    /// Interaction logic for ClipboardWnd.xaml
    /// </summary>
    public partial class ClipboardWnd : Window
    {
        IntPtr _ClipboardViewerNext = IntPtr.Zero;
        Core.GlobalKeyboardHook hook;
        private const int WM_DRAWCLIPBOARD = 0x0308;

        public ClipboardWnd()
        {
            InitializeComponent();

            this.Left = this.Width - this.Width;
            this.Top = this.Height - this.Height;

            BtnClear_MouseLeftButtonUp(null, null);

            hook = new Core.GlobalKeyboardHook();
            hook.KeyboardPressed += Hook_KeyboardPressed;
        }

        public void AddData(IDataObject data)
        {
            //var cache = ClipboardCache.Load();
            //cache.Objects.Insert(0, new ClipboardDataForCache(data, DateTime.Now));
            //cache.Save();

            //if (this.Visibility == Visibility.Visible)
            if (Core.Settings.Base.Clipboard.IgnoreFiles && data.GetDataPresent(DataFormats.FileDrop))
                return;

            if(!DoWeHave(data))
                AddUI(data, DateTime.Now);
        }

        public void AddUI(IDataObject data, DateTime time)
        {
            ClipboardPanel.Children.Insert(0,
                new ClipboardElement(this, data, time)
                );
            
            if(ClipboardPanel.Children.Count >= Core.Settings.Base.Clipboard.MaximumEntries)
            {
                var last = ClipboardPanel.Children[Core.Settings.Base.Clipboard.MaximumEntries - 1];

                ClipboardPanel.Children.Remove(last);

                ((ClipboardElement)last).Free();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public bool DoWeHave(IDataObject data)
        {
            foreach(var e in ClipboardPanel.Children)
            {
                ClipboardElement elem = e as ClipboardElement;

                if (elem.Data == data)
                    return true;
            }
            return false;
        }

        public void UIFromCache(List<ClipboardDataForCache> c)
        {
            foreach (var e in c)
                ClipboardPanel.Children.Add(new ClipboardElement(this, e.Data, e.Time));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == WM_DRAWCLIPBOARD)
            {
                handled = true;
                AddData(Clipboard.GetDataObject());
            }

            return IntPtr.Zero;
        }

        public void ShowEx()
        {
            double screenWidth = SystemParameters.VirtualScreenWidth;
            double screenHeight = SystemParameters.VirtualScreenHeight;

            var cy = System.Windows.Forms.Cursor.Position.Y;
            var cx = System.Windows.Forms.Cursor.Position.X;

            var left = (double)cx;
            var top = (double)cy;

            if (left + this.Width > screenWidth)
                left -= this.Width;

            if (top + this.Height> screenHeight)
                top -= this.Height;

            this.Left = left;
            this.Top = top;

            // wpf gay
            this.Visibility = Visibility.Visible;
            this.Show();
            this.Activate();

            PanelScroll.ScrollToTop();

            DoAero();
        }

        private void Hook_KeyboardPressed(object sender, Core.GlobalKeyboardHookEventArgs e)
        {
            if(e.KeyboardState == Core.GlobalKeyboardHook.KeyboardState.SysKeyDown &&
                e.Key == Key.V &&
                Core.Keyboard.IsKeyDown(Core.Core.SuperKey))
            {
                e.Handled = true;
                ShowEx();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        
        public void DoAero()
        {
            // idk why but it's broken
            //if (Core.DwmApi.CheckAeroEnabled())
            //{
            //    Core.DwmApi.Glass(this);
            //    this.Background = new SolidColorBrush(Colors.Transparent);
            //}
            //else
            {
                this.Background = new SolidColorBrush(Colors.White);
                // no aero
                // improve me
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);

            source.AddHook(new HwndSourceHook(WndProc));

            Core.ClipboardHook.SetClipboardViewer(source.Handle);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearUI();
            ClipboardCache.CacheFile.Delete();
        }

        public void ClearUI()
        {
            foreach (var i in ClipboardPanel.Children)
            {
                ClipboardElement ec = (ClipboardElement)i;
                ec.Free();
            }

            ClipboardPanel.Children.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DoAero();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (this.Visibility == Visibility.Visible)
            //{
            //    DoAero();
                
            //    var cache = ClipboardCache.Load();

            //    foreach (var eo in cache.Objects)
            //        AddUI(eo.Data, eo.Time);
            //}
            //else
            //{
            //    ClearUI();
            //}
        }
    }
}
