using BlImplementation;
namespace BlTest;

internal class Program
{
    public static void Display()
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.OrderItem orderItem = new BO.OrderItem();
        BO.Order order = new BO.Order();
        int flag = 1;
        while (flag != 0)
        {
            Console.WriteLine(@"Choose one of the several options: 0. Exit
                                   1. Test Products
                                   2. Test Orders
                                   3. Test Cart");
            int chosen = 0;
            int.TryParse(Console.ReadLine(), out chosen);
            switch (chosen)
            {
                case 0:
                    flag = 0;
                    Console.WriteLine("Thank you and Good Bye!");
                    break;
                case 1:
                    Console.WriteLine(@"Choose one of the several options: 1. Add a product
                                   2. Get a product for Director
                                   3. Get products List
                                   4. Update Product
                                   5. Delete Prodct
                                   6. Get a product for Client");
                    int chosen2 = 0;
                    int.TryParse(Console.ReadLine(), out chosen2);
                    switch (chosen2)
                    {
                        case 1:
                            BO.Product product = new BO.Product();
                            Console.WriteLine("Enter the id of your new product");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the name of your new product");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter the price of your new product");
                            double price = 0;
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Choose the cathegory of your new product: 0.Men, 1. Women, 2. children");
                            string choice = Console.ReadLine();
                            BO.Category category = (BO.Category)Convert.ToInt32(choice);
                            Console.WriteLine("Enter the stock of your products");
                            int stock = 0;
                            int.TryParse(Console.ReadLine(), out stock);
                            product.ID = id;
                            product.Name = name;
                            product.Price = price;
                            product.InStock = stock;
                            product.Category = category;
                            try
                            {
                            bl?.Product.Add(product);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the product that you want: ");
                            int Newid = 0;
                            int.TryParse(Console.ReadLine(), out Newid);
                            try
                            {
                            Console.WriteLine(bl?.Product.GetDirector(Newid).ToString());
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 3:
                            
                            Console.WriteLine(@"Sort your list of product by: 1: All
                                2: Category
                                3: By Brands
                                4: Most to least expensive
                                5: Least to most expensive
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in bl?.Product.GetProductForLists())
                                    {
                                        Console.WriteLine(item.ToString());
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: Men------------- 2: Women -------------- 3: Children-----------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        Func<BO.ProductForList?, bool>? optionaldel1 = (Prod) => Prod?.Category == BO.Category.Men;
                                        foreach (var item in bl?.Product.GetProductForLists(optionaldel1))
                                        {
                                            if (item?.ID != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        Func<BO.ProductForList?, bool>? optionaldel2 = (Prod) => Prod?.Category == BO.Category.Women;
                                        foreach (var item in bl?.Product.GetProductForLists(optionaldel2))
                                        {
                                            if (item?.ID != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 3)
                                    {
                                        Func<BO.ProductForList?, bool>? optionaldel3 = (Prod) => Prod?.Category == BO.Category.Children;
                                        foreach (var item in bl?.Product.GetProductForLists(optionaldel3))
                                        {
                                            if (item?.ID != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    string prodName;
                                    prodName = Console.ReadLine();
                                    Func<BO.ProductForList?, bool>? optionaldel = (Prod) => Prod?.Name == prodName;
                                    foreach (var item in bl?.Product.GetProductForLists(optionaldel))
                                    {
                                        if (item?.ID != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 4:
                                    foreach (var item in bl?.Product.GetProductForLists().OrderByDescending(product => product?.Price).ToList())
                                    {
                                        if (item?.ID != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 5:
                                    foreach (var item in bl?.Product.GetProductForLists().OrderBy(product => product?.Price).ToList())
                                    {
                                        if (item?.ID != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 4:
                            BO.Product product1 = new BO.Product();
                            Console.WriteLine("Enter the id of the product you want update");
                            int id1 = 0;
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine("Enter the new  name of the product you want");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Enter the new  price of the product you want");
                            double price1 = 0;
                            double.TryParse(Console.ReadLine(), out price1);
                            Console.WriteLine("Enter the new cathegory of the product you want: 0.Men, 1. Women, 2. children");
                            string choice1 = Console.ReadLine();
                            BO.Category category1 = (BO.Category)Convert.ToInt32(choice1);
                            Console.WriteLine("Enter the new stock of the product you want");
                            int stock1 = 0;
                            int.TryParse(Console.ReadLine(), out stock1);
                            product1.ID = id1;
                            product1.Name = name1;
                            product1.Price = price1;
                            product1.Category = category1;
                            product1.InStock = stock1;
                            try
                            {
                            bl?.Product.Update(product1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 5:
                            Console.WriteLine("Enter the id of the product that you want to delete");
                            int id2 = 0;
                            int.TryParse(Console.ReadLine(), out id2);
                            try
                            {
                            bl?.Product.Delete(id2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 6:
                            Console.WriteLine("Enter the id of the product that you want:");
                            int id3 = 0;
                            int.TryParse(Console.ReadLine(), out id3);
                            Console.WriteLine("Enter your Order Id:");
                            int id4 = 0;
                            int.TryParse(Console.ReadLine(), out id4);
                            try
                            {
                                BO.Cart cart1 = new BO.Cart()
                                {
                                    CustomerAddress = bl?.Order.GetOrderItem(id4).CustomerAdress,
                                    CustomerEmail = bl?.Order.GetOrderItem(id4).CustomerEmail,
                                    CustomerName = bl?.Order.GetOrderItem(id4).CustomerName,
                                    orderItems = bl?.Order.GetOrderItem(id4).orderItems,
                                    TotalPrice = (double)bl?.Order.GetOrderItem(id4).TotalPrice!
                                };

                                try
                                {
                                    Console.WriteLine(bl?.Product.GetClient(id3, cart1).ToString());
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine(@"Choose one of the several options: 1. Get Order List
                                   2. Get an Order Item
                                   3. Update order to shipping status
                                   4. Update order to received status
                                   5. Track the order
                                   6. Update the Order");

                    int chosen3 = 0;
                    int.TryParse(Console.ReadLine(), out chosen3);
                    switch (chosen3)
                    {
                        case 1:
                            
                            Console.WriteLine(@"Sort your list of order by: 1: All
                                2: By CustomerName 
                                3: By OrderStatus
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in bl?.Order.GetOrderList())
                                    {
                                        Console.WriteLine(item.ToString());
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: CustomerName in order------------- 2: CustomerName Descending--------------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        foreach (var item in bl?.Order.GetOrderList().OrderBy(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.ID != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        foreach (var item in bl?.Order.GetOrderList().OrderByDescending(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.ID != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("1: Approved------------- 2: Expedited-------------- 3: Received-------------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        Func<BO.OrderForList?, bool>? optionaldel = (Order) => Order?.Status == BO.OrderStatus.Approved;
                                        foreach (var item in bl?.Order.GetOrderList(optionaldel))
                                        {
                                            if (item?.ID != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        Func<BO.OrderForList?, bool>? optionaldel = (Order) => Order?.Status == BO.OrderStatus.Expedited;
                                        foreach (var item in bl?.Order.GetOrderList(optionaldel))
                                        {
                                            if (item?.ID != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 3)
                                    {
                                        Func<BO.OrderForList?, bool>? optionaldel = (Order) => Order?.Status == BO.OrderStatus.Received;
                                        foreach (var item in bl?.Order.GetOrderList(optionaldel))
                                        {
                                            if (item?.ID != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the Order you want");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            try
                            {
                            Console.WriteLine(bl?.Order.GetOrderItem(orderid).ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter the id of the Order you want update to shipping status");
                            int orderid1 = 0;
                            int.TryParse(Console.ReadLine(), out orderid1);
                            try
                            {
                            Console.WriteLine(bl?.Order.UpdateOrderShipping(orderid1).ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter the id of the Order you want update to Received status");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            try
                            {
                            Console.WriteLine(bl?.Order.UpdadteOrderReceived(orderid2).ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 5:
                            Console.WriteLine("Enter the id of the Order you want update to Track");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            try
                            {
                            Console.WriteLine(bl?.Order.TrackingOrder(orderid3).ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                    case 3:
                            Console.Write("Enter your name:");
                            string customerName1 = Console.ReadLine();
                            Console.Write("Enter the Customer Email: ");
                            string customerEmail1 = Console.ReadLine();
                            Console.Write("Enter the Customer Address: ");
                            string customerAddress1 = Console.ReadLine();
                            BO.Cart cart = new BO.Cart();
                            cart.CustomerName = customerName1;
                            cart.CustomerEmail = customerEmail1;
                            cart.CustomerAddress = customerAddress1;
                            bool stop= false;
                    while(!stop)
                    {
                    Console.WriteLine(@"Choose one of the several options: 1. Add a Product to Cart
                                   2. Update amount of Product in Cart
                                   3. Cart confirmation 
                                   ");
                    int chosen4 = 0;
                    int.TryParse(Console.ReadLine(), out chosen4);
                    switch (chosen4)
                    {
                        case 1:
                            Console.WriteLine("Enter the id of the product that you want:");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id);
                            //Utiliser getOrderItem(orderItemId1) pour remplir cart.Items
                            cart.TotalPrice =0;
                                try
                                {
                                  bl?.Cart.Add(cart, id);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    stop=true;
                                }
                            break;

                        case 2:
                                Console.WriteLine("Enter the id of the product that you want:");
                                int id1 = 0;
                                int.TryParse(Console.ReadLine(),out id1);
                                Console.WriteLine("Enter the new Amount:");
                                int newAmount = 0;
                                int.TryParse(Console.ReadLine(),out newAmount);
                                try
                                {
                                     bl?.Cart.Update(cart, id1, newAmount);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            break;

                        case 3:
                                try
                                {
                                    bl?.Cart.Confirmation(cart);
                                    Console.WriteLine("Your Order was confirmed");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                stop=true;
                            break;

                        default:
                            break;
                            //}
                    }

                    }
                    break;
                default:
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        Display();
    }
}
