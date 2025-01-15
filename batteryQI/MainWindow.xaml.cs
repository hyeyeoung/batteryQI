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
using batteryQI.Pages;
using Microsoft.Win32;
using ScottPlot;

namespace batteryQI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double[] dataX1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            double[] dataY1 = { 90, 97, 95, 99, 98, 95, 94, 94, 93, 95, 96, 93 };

            double[] dataX2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            double[] dataY2 = { 4, 5, 2, 3, 5, 3, 2, 6, 4, 3, 2, 4, 3, 2, 4, 5, 2, 3, 5, 6, 3, 4, 1, 4 };

            double[] dataX3 = { 1, 2, 3, 4};
            double[] dataY3 = { 20, 30, 14, 36};

            WpfPlot1.Plot.Add.Scatter(dataX1, dataY1);
            WpfPlot2.Plot.Add.Scatter(dataX2, dataY2);
            WpfPlot3.Plot.Add.Bars(dataX3, dataY3);

            WpfPlot1.Refresh();
            WpfPlot2.Refresh();
            WpfPlot3.Refresh();
        }

        private void Border_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void ImageSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImgPath = openFileDialog.FileName;

                PreviewImage.Source = new BitmapImage(new Uri(selectedImgPath));
            }
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Pages/ImagePage.xaml", UriKind.Relative));
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Pages/ChartPage.xaml", UriKind.Relative));
        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Pages/ManagerPage.xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}