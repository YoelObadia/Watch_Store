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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window, INotifyPropertyChanged
    {
        BlApi.IBl? bl;
        private BO.Order _order = new BO.Order();

        public event PropertyChangedEventHandler? PropertyChanged;

        public BO.Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Order)));
            }
        }
        public OrderWindow(BO.Order order1)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            Order = order1;
        }

        private void UpdateShippingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Order.UpdateOrderShipping(Order.Id);
                MessageBox.Show("Order updated to Shipping status with success!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateReceivedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Order.UpdadteOrderReceived(Order.Id);
                MessageBox.Show("Order updated to Received status");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Order.DeletOrderForAdmin(Order.Id);
                MessageBox.Show("Order removed from the list with success!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
