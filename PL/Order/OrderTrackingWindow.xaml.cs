using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<DateTime?> _ordersTrackingList = new ObservableCollection<DateTime?>();
        public ObservableCollection<DateTime?> OrderTrackingList 
        {
            get { return _ordersTrackingList; }
            set { _ordersTrackingList = value; }
        } 

        private OrderTracking _OrderTracking = new OrderTracking();  
        public OrderTracking orderTracking
        {
            get { return _OrderTracking; }
            set 
            {
                _OrderTracking = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(orderTracking)));
            }
        }

        BlApi.IBl? bl;

        public event PropertyChangedEventHandler? PropertyChanged;

        public OrderTrackingWindow(string text)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            try
            {
                orderTracking = bl?.Order.TrackingOrder(Convert.ToInt32(text))!;

                DateTime? dateTime = (orderTracking.OrderTrackingList![0]!.Item1);
                DateTime? dateTime1 = (orderTracking.OrderTrackingList[1]!.Item1);
                DateTime? dateTime2 = (orderTracking.OrderTrackingList[2]!.Item1);
                OrderTrackingList = new ObservableCollection<DateTime?>
                {
                    dateTime,
                    dateTime1,
                    dateTime2
                };
                lstview1.ItemsSource = OrderTrackingList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
