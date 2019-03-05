using System;
using System.Collections.Generic;
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
        public ClipboardElement()
        {
            InitializeComponent();
        }

        public ClipboardElement(IDataObject data, DateTime time)
        {
            InitializeComponent();
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
                    Set(new cDataText((string)value.GetData(DataFormats.Text)));
                else if (value.GetDataPresent(DataFormats.Bitmap))
                    Set(new cDataImage((System.Windows.Interop.InteropBitmap)value.GetData(DataFormats.Bitmap)));
            }
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
    }
}
