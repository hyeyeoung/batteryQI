using batteryQI.ViewModels;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace batteryQI.UserControls
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerView : UserControl
    {
        public ManagerView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }



        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
