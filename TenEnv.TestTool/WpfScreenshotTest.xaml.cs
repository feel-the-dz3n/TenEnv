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
using System.Windows.Shapes;

namespace TenEnv.TestTool
{
    /// <summary>
    /// Interaction logic for WpfScreenshotTest.xaml
    /// </summary>
    public partial class WpfScreenshotTest : Window
    {
        public WpfScreenshotTest()
        {
            InitializeComponent();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            img.Source = TenEnv.ModernScreenshot.ScreenshotWnd.GetScreenshot();
        }
    }
}
