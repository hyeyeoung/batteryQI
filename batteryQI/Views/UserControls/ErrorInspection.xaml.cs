﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace batteryQI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ErrorInspection.xaml
    /// </summary>
    public partial class ErrorInspection : UserControl
    {
        public ErrorInspection()
        {
            InitializeComponent();
        }

        private void NomalButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ErrorButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
