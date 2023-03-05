using PL.Order;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl;

        public MainWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
        }


        private void AdminButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

        private void NewOrderButton_Click(object sender, RoutedEventArgs e) => new ProductItemWindow().Show();

        private void SimulatorButton_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().Show();
        private void TrackOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingWindow(OrderToTrack.Text).Show();
            OrderToTrack.Text = "";
        }
    }
}
