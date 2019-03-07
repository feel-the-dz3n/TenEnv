using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TenEnv.ModernClipboard
{
    /// <summary>
    /// Interaction logic for ClipboardElement.xaml
    /// </summary>
    public partial class ClipboardElement : UserControl
    {
        ClipboardWnd window = null;

        public ClipboardElement()
        {
            InitializeComponent();
        }

        public ClipboardElement(ClipboardWnd w, IDataObject data, DateTime time)
        {
            InitializeComponent();
            window = w;
            TimeStamp = time;
            Data = data;
        }

        public DateTime TimeStamp
        {
            get => (DateTime)tbTime.Tag;
            set
            {
                tbTime.Tag = value;
                tbTime.Text = value.ToString("HH:mm");
            }
        }

        public IDataObject Data
        {
            get => (IDataObject)GridForData.Tag;
            set
            {
                GridForData.Tag = value;

                if (value == null)
                    return;

                if (value.GetDataPresent(DataFormats.Text))
                    Set(new cDataStringLike((string)value.GetData(DataFormats.Text)));
                else if (value.GetDataPresent(DataFormats.Bitmap))
                {
                    Set(new cDataImage((System.Windows.Interop.InteropBitmap)value.GetData(DataFormats.Bitmap)));
                    tbTime.Foreground = new SolidColorBrush(Colors.White);
                }
                else if (value.GetDataPresent(DataFormats.Rtf))
                    Set(new cDataStringLike((string)value.GetData(DataFormats.Rtf), "RTF"));
                else if (value.GetDataPresent(DataFormats.Html))
                    Set(new cDataStringLike((string)value.GetData(DataFormats.Html), "HTML"));
                else if (value.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])value.GetData(DataFormats.FileDrop);

                    string cs = files.Length + " files";
                    if (files.Length == 1)
                        cs = "File";

                    Set(new cDataStringLike(JoinFileString(files), cs));
                }
                else
                    Set(new cDataStringLike($"Can't display preview for: {value.GetFormats().FirstOrDefault()})"));
            }
        }

        private string JoinFileString(string[] files)
        {
            StringBuilder a = new StringBuilder();

            foreach(var file in files)
            {
                a.AppendLine("...\\" + new FileInfo(file).Name);
            }

            return a.ToString();
        }

        public void Free()
        {
            if(GridForData.Children.Count >= 1)
            {
                var c = GridForData.Children[0];
                var free = c.GetType().GetMethod("Free");

                if (free != null)
                    free.Invoke(c, new object[] { });
            }

            GridForData.Children.Clear();

            Data = null;
        }

        public void Set(UIElement e = null)
        {
            GridForData.Children.Clear();

            if(e != null)
                GridForData.Children.Add(e);
        }

        private void GridMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ClipboardPanel.Children.Remove(this);
            this.Free();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void GridMain_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOverGrid.Visibility = Visibility.Visible;
        }

        private void GridMain_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverGrid.Visibility = Visibility.Collapsed;
        }
    }
}
