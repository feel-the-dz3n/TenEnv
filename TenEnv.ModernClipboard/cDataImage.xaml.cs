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
    /// Interaction logic for cDataImage.xaml
    /// </summary>
    public partial class cDataImage : UserControl
    {
        public cDataImage(ImageSource img = null)
        {
            InitializeComponent();
            Img.Source = img;
        }

        public cDataImage(System.Drawing.Bitmap img = null)
        {
            InitializeComponent();
            Img.Source = Core.Core.ImageSourceFromBitmap(img);
        }

        public void Free()
        {
            Img.Source = null;
        }
    }
}
