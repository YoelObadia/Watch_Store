using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private BO.Cart? Cart = new BO.Cart();
        public ObservableCollection<OrderItem?> OrderItems { get; set; } = new ObservableCollection<OrderItem>()!;
        BlApi.IBl? bl;
        public CartWindow(BO.Cart cart)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            Cart = cart;
            OrderItems = new ObservableCollection<OrderItem?>(from item in Cart.orderItems
                           select item)!;
            lstView.ItemsSource = OrderItems;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Cart.Update(Cart!, Convert.ToInt32(ProductID.Text), Convert.ToInt32(QuantityChoice.Text));
                OrderItems = new ObservableCollection<OrderItem?>(from item in Cart.orderItems
                                                                  select item)!;
                lstView.ItemsSource = OrderItems;
                MessageBox.Show("Updated with success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CartConfirmationWindow(Cart!).ShowDialog();
            Close();
        }
    }
}
