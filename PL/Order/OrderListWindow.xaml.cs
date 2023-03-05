using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl;
        public ObservableCollection<OrderForList?> orderForLists { get; set; } = new ObservableCollection<OrderForList>()!;
        public OrderListWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            orderForLists = new ObservableCollection<OrderForList?>(bl?.Order.GetOrderList()!);
            lstView.ItemsSource = orderForLists;
            
        }

        private void OrderListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList orderForList = (OrderForList)lstView.SelectedItem;
            BO.Order order = new BO.Order();
            order = bl?.Order.GetOrderItem(orderForList.ID)!;
            OrderWindow orderWindow = new OrderWindow(order!);
            orderWindow.ShowDialog();
            orderForLists = new ObservableCollection<OrderForList?>(bl?.Order.GetOrderList()!);
            lstView.ItemsSource = orderForLists;
        }

    }
}
