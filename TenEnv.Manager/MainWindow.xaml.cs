﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (var app in TenApps.Apps)
            {
                lbTenApps.Children.Add(new TenAppEntry() { App = app });
            }
        }

        private void BtnRunAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var app in lbTenApps.Children.OfType<TenAppEntry>())
                app.BtnStart_Click(null, null);

            btnRunAll.IsEnabled = false;
        }
    }
}
