using PL.Product;
using System;
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
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Logique d'interaction pour CartConfirmationWindow.xaml
    /// </summary>
    public partial class CartConfirmationWindow : Window
    {
        public BO.Cart? Cart = new BO.Cart();
        BlApi.IBl? bl;
        public CartConfirmationWindow(BO.Cart cart1)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            Cart = cart1;
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cart!.CustomerName = NameTextBox.Text;
                Cart!.CustomerAddress = AddressTextBox.Text;
                Cart!.CustomerEmail = EmailTextBox.Text;
                bl?.Cart.Confirmation(Cart!);
                MessageBox.Show("Your cart is confirmed!");
                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }

}
