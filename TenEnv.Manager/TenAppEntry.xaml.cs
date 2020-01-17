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

namespace TenEnv.Manager
{
    /// <summary>
    /// Interaction logic for TenAppEntry.xaml
    /// </summary>
    public partial class TenAppEntry : UserControl
    {
        private TenApp app;

        public TenAppEntry()
        {
            InitializeComponent();
        }

        public TenApp App
        {
            get => app;
            set
            {
                app = value;

                tbAppName.Text = value.Title.Title;
                tbAppDescription.Text = value.Description.Description;
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            App.CreateInstance();
            btnStart.IsEnabled = false;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            App.Stop();
        }
    }
}
