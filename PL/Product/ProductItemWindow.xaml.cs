using BO;
using DO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl? bl;

        public BO.Cart? cart = new BO.Cart();
        static Random rand = new Random();
        public ObservableCollection<ProductForList?> ProductForLists { get; set; } = new ObservableCollection<ProductForList>()!;
        public ObservableCollection<BO.Product?> Products { get; set; } = new ObservableCollection<BO.Product>()!;
        public ObservableCollection<ProductItem?> ProductItems { get; set; } = new ObservableCollection<ProductItem>()!;
        public ProductItemWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            ProductForLists = new ObservableCollection<ProductForList?>(bl?.Product?.GetProductForLists()!);
            Products = new ObservableCollection<BO.Product>(from item in ProductForLists
                                                            select bl?.Product.GetDirector(item.ID))!;
            ProductItems = new ObservableCollection<ProductItem>(Products.Select(
                pi => new ProductItem
                {
                    ID = pi!.ID,
                    Name = pi?.Name,
                    Price = pi!.Price,
                    Category = pi.Category,
                    Amount = pi.InStock,
                    InStock = pi.InStock > 0
                }))!;
            lstView.ItemsSource = ProductItems;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cart.CustomerName = "";
            cart.CustomerEmail = "";
            cart.CustomerAddress = "";
            cart.orderItems = new List<OrderItem?>();
            cart.TotalPrice = 0;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = CategorySelector.SelectedItem;
            if (BO.Category.All == (BO.Category)selectedItem)
            {
                ProductForLists = new ObservableCollection<ProductForList?>(bl?.Product?.GetProductForLists()!);
                lstView.ItemsSource = ProductForLists;
            }
            else
            {
                ProductForLists = new ObservableCollection<ProductForList?>(bl?.Product.GetProductForLists(P => P?.Category == (BO.Category)selectedItem)!);
                lstView.ItemsSource = ProductForLists;
            }
        }

        private void ShowCartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart!).ShowDialog();

            ProductForLists = new ObservableCollection<ProductForList?>(bl?.Product?.GetProductForLists()!);
            Products = new ObservableCollection<BO.Product>(from item in ProductForLists
                                                            select bl?.Product.GetDirector(item.ID))!;
            ProductItems = new ObservableCollection<ProductItem>(Products.Select(
                pi => new ProductItem
                {
                    ID = pi!.ID,
                    Name = pi?.Name,
                    Price = pi!.Price,
                    Category = pi.Category,
                    Amount = pi.InStock,
                    InStock = pi.InStock > 0
                }))!;
            cart = new BO.Cart();
            lstView.ItemsSource = ProductItems;
        }

        private void AddProductItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductItem productItem = (lstView.SelectedItem as ProductItem)!;

            try
            {
                bl?.Cart.Add(cart!, productItem.ID);
                MessageBox.Show("Your product is added in your cart!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}