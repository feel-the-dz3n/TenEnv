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

namespace TenEnv.ModernScreenshot
{
    /// <summary>
    /// Interaction logic for ControlButtons.xaml
    /// </summary>
    public partial class ControlButtons : UserControl
    {
        public ScreenshotWnd MainWnd;

        public ControlButtons()
        {
            InitializeComponent();
        }

        private void RectFormBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FreeFromBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FullScreenBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWnd.Finish(MainWnd.imgScreen.Source);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWnd.Close();
        }
    }
}
