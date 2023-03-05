using Dal;
using DalApi;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace DO;
static class Program
{
    public static void Display()
    {
        DalApi.IDal? dal = DalApi.Factory.Get();
        int flag = 1;
        while (flag != 0)
        {
            Console.WriteLine(@"Choose one of the several options: 0. Exit
                                   1. Test Products
                                   2. Test Orders
                                   3. Test OrderItem");

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
                                   2. Get a product
                                   3. Get products List
                                   4. Update Product
                                   5. Delete Prodct 
                                   6. Get a specific Product ");

                    int chosen2 = 0;
                    int.TryParse(Console.ReadLine(), out chosen2);
                    switch (chosen2)
                    {
                        case 1:
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
                            Category category = (Category)Convert.ToInt32(choice);
                            Console.WriteLine("Enter the stock of your products");
                            int stock = 0;
                            int.TryParse(Console.ReadLine(), out stock);
                            Products p = new Products();
                            p.Name = name;
                            p.Id = id;
                            p.Price = price;
                            p.InStock = stock;
                            p.Category = category;
                            dal?.Product.Add(p);
                            break;

                        case 2:
                            Console.WriteLine("Enter the id of the product that you want: ");
                            int Newid = 0;
                            int.TryParse(Console.ReadLine(), out Newid);
                            Console.WriteLine(dal?.Product.Get(Newid, 0).ToString());
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of product by: 1: All
                                2: Category
                                3: By Brands
                                4: Least to most expensive
                                5: Most to least expensive
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal?.Product.GetList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: Men------------- 2: Women -------------- 3: Children-----------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        Func<Products?, bool>? optionaldel = (Prod) => Prod?.Category == Category.Men;
                                        foreach (var item in dal?.Product.GetList(optionaldel))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        Func<Products?, bool>? predicate = (Prod) => Prod?.Category == Category.Women;
                                        foreach (var item in dal?.Product.GetList(predicate))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 3)
                                    {
                                        Func<Products?, bool>? predicate1 = (Prod) => Prod?.Category == Category.Children;
                                        foreach (var item in dal?.Product.GetList(predicate1))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    string prodName;
                                    prodName = Console.ReadLine()!;
                                    Func<Products?, bool>? predicate2 = (Prod) => Prod?.Name == prodName;
                                    foreach (var item in dal?.Product.GetList(predicate2))
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 4:
                                    foreach (var item in dal?.Product.GetList().OrderByDescending(product => product?.Price).ToList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 5:
                                    foreach (var item in dal?.Product.GetList().OrderBy(product => product?.Price).ToList())
                                    {
                                        if (item?.Id != 0)
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
                            Console.WriteLine("Enter the id of the product that you want to update");
                            int id2 = 0;
                            int.TryParse(Console.ReadLine(), out id2);
                            // toute les saisis du update doivent etre dans le main
                            Console.WriteLine("Enter the new  name of the product you want");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Enter the new  price of the product you want");
                            double price1 = 0;
                            double.TryParse(Console.ReadLine(), out price1);
                            Console.WriteLine("Enter the new cathegory of the product you want: 0.Men, 1. Women, 2. children");
                            string choice2 = Console.ReadLine();
                            Category category1 = (Category)Convert.ToInt32(choice2);
                            Console.WriteLine("Enter the new stock of the product you want");
                            int stock1 = 0;
                            int.TryParse(Console.ReadLine(), out stock1);
                            Products products = new Products
                            {
                                Id = id2,
                                Name = name1,
                                Price = price1,
                                Category = category1,
                                InStock = stock1
                            };
                            dal?.Product.Add(products);
                            dal?.Product.Update(id2, 0);
                            break;

                        case 5:
                            Console.WriteLine("Enter the id of the product that you want to delete");
                            int id3 = 0;
                            int.TryParse(Console.ReadLine(), out id3);
                            dal?.Product.Delete(id3, 0);
                            break;
                        case 6:
                            Console.WriteLine("Enter the id of your product");
                            int pId = 0;
                            int.TryParse(Console.ReadLine(), out pId);
                            Console.WriteLine("Enter the name of your product");
                            string pName = Console.ReadLine();
                            Console.WriteLine("Enter the price of your product");
                            double pPrice = 0;
                            double.TryParse(Console.ReadLine(), out pPrice);
                            Console.WriteLine("Choose the cathegory of your product: 0.Men, 1. Women, 2. children");
                            string pChoice = Console.ReadLine();
                            Category pCategory = (Category)Convert.ToInt32(pChoice);
                            Console.WriteLine("Enter the stock of your product");
                            int pStock = 0;
                            int.TryParse(Console.ReadLine(), out pStock);
                            Products p1 = new Products();
                            p1.Id = pId;
                            p1.Name = pName;
                            p1.Price = pPrice;
                            p1.Category = pCategory;
                            p1.InStock = pStock;
                            Func<Products?, bool>? func = p => (p?.Id == p1.Id && p?.Name == p1.Name && p?.Price == p1.Price && p?.Category == p1.Category && p?.InStock == p1.InStock);
                            Console.WriteLine(dal?.Product.GetItem(func).ToString());
                            break;
                        default:
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine(@"Choose one of the several options: 1. Add an order
                                   2. Get an order
                                   3. Get order List
                                   4. Update order
                                   5. Delete order
                                   6. Get a specefic order");

                    int chosen3 = 0;
                    int.TryParse(Console.ReadLine(), out chosen3);
                    TimeSpan duration = new TimeSpan(3, 0, 0, 0);

                    switch (chosen3)
                    {
                        case 1:
                            // mettre le pelet des order    
                            Console.WriteLine("Enter an Id: ");
                            int OrderId = 0;
                            int.TryParse(Console.ReadLine(), out OrderId);
                            Console.WriteLine("Enter the Customer Name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter customer Email: ");
                            string Email = Console.ReadLine();
                            Console.WriteLine("Enter Customer adress: ");
                            string Adress = Console.ReadLine();
                            DateTime orderDate = DateTime.Now;
                            DateTime shipdate = orderDate.Add(duration);
                            DateTime deliveryDate = shipdate.Add(duration);
                            Order ord = new Order();
                            ord.Id = OrderId;
                            ord.OrderDate = orderDate;
                            ord.ShipDate = shipdate;
                            ord.DeliveryDate = deliveryDate;
                            ord.CustomerName = name;
                            ord.CustomerEmail = Email;
                            ord.CustomerAdress = Adress;
                            dal?.Order.Add(ord);
                            break;

                        case 2:
                            Console.WriteLine("Enter the id of the order that you want: ");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine(dal?.Order.Get(orderid, 0));
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of order by: 1: All
                                2: By CustomerName 
                                3: By Date
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal?.Order.GetList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: CustomerName in order------------- 2: CustomerName Descending--------------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        foreach (var item in dal?.Order.GetList().OrderBy(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        foreach (var item in dal?.Order.GetList().OrderByDescending(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("1: OrderDate------------- 2: ShipDate-------------- 3: DeliveryDate-------------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        foreach (var item in dal?.Order.GetList().OrderBy(order => order?.OrderDate).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        foreach (var item in dal?.Order.GetList().OrderByDescending(order => order?.ShipDate).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 3)
                                    {
                                        foreach (var item in dal?.Order.GetList().OrderByDescending(order => order?.DeliveryDate).ToList())
                                        {
                                            if (item?.Id != null)
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

                        case 4:
                            Console.WriteLine("Enter the id of the product that you want to update:");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            Console.WriteLine("Enter the Customer new Name: ");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Enter customer new Email: ");
                            string Email1 = Console.ReadLine();
                            Console.WriteLine("Enter Customer new adress: ");
                            string Adress1 = Console.ReadLine();
                            Order ord1 = new Order();
                            ord1.Id = orderid2;
                            ord1.CustomerName = name1;
                            ord1.CustomerEmail = Email1;
                            ord1.CustomerAdress = Adress1;
                            dal?.Order.Add(ord1);
                            dal?.Order.Update(orderid2, 0);
                            break;

                        case 5:
                            Console.WriteLine("Enter the id that you want to delete:");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            dal?.Order.Delete(orderid3, 0);
                            break;
                        case 6:
                            Console.WriteLine("Enter an Id: ");
                            int orderId = 0;
                            int.TryParse(Console.ReadLine(), out orderId);
                            Console.WriteLine("Enter the Customer Name: ");
                            string orderName = Console.ReadLine()!;
                            Console.WriteLine("Enter customer Email: ");
                            string orderEmail = Console.ReadLine()!;
                            Console.WriteLine("Enter Customer adress: ");
                            string orderAdress = Console.ReadLine()!;
                            Order order = new Order();
                            order.Id = orderId;
                            order.CustomerName = orderName;
                            order.CustomerEmail = orderEmail;
                            order.CustomerAdress = orderAdress;
                            Func<Order?, bool>? func = o => (o?.Id == order.Id && o?.CustomerName == order.CustomerName && o?.CustomerEmail == order.CustomerEmail && o?.CustomerAdress == order.CustomerAdress);
                            Console.WriteLine(dal?.Order.GetItem(func).ToString());
                            break;
                        default:
                            break;
                    }
                    break;

                case 3:
                    Console.WriteLine(@"Choose one of the several options: 1. Add an Orderitem
                                   2. Get an orderitem
                                   3. Get orderitem List
                                   4. Update orderitem
                                   5. Delete orderitem ");

                    int chosen4 = 0;
                    int.TryParse(Console.ReadLine(), out chosen4);
                    switch (chosen4)
                    {
                        case 1:
                            Console.WriteLine("enter the OrderId: ");
                            int ordId = 0;
                            int.TryParse(Console.ReadLine(), out ordId);
                            Console.WriteLine("Enter the prodId: ");
                            int productId = 0;
                            int.TryParse(Console.ReadLine(), out productId);
                            Console.WriteLine("Enter the amount: ");
                            int Amount = 0;
                            int.TryParse(Console.ReadLine(), out Amount);
                            OrderItems orderitems = new OrderItems();
                            orderitems.OrderId = ordId;
                            orderitems.ProductId = productId;
                            orderitems.Amount = Amount;
                            orderitems.Price = ((double)dal?.Product.Get(productId, 0).Price!) * Amount;
                            dal?.OrderItem.Add(orderitems);
                            break;

                        case 2:
                            Console.WriteLine("Enter the order id of the orderitem that you want: ");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine("Enter the product id of the orderitem that you want: ");
                            int prodId = 0;
                            int.TryParse(Console.ReadLine(), out prodId);
                            Console.WriteLine(dal?.OrderItem.Get(orderid, prodId));
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of order by: 1: All
                                    2: By Price 
                                    3: By Amount
                                    ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal?.OrderItem.GetList()!)
                                    {
                                        if (item?.ProductId != 0 && item?.OrderId != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: Ordered Price------------- 2: Descending Price--------------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        foreach (var item in dal?.OrderItem.GetList().OrderBy(orderItem => orderItem?.Price).ToList()!)
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        foreach (var item in dal?.OrderItem.GetList().OrderByDescending(orderItem => orderItem?.Price).ToList()!)
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("1: Ordered Amount------------- 2: Descending Amount--------------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        foreach (var item in dal?.OrderItem.GetList().OrderBy(orderItem => orderItem?.Amount).ToList()!)
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        foreach (var item in dal?.OrderItem.GetList().OrderByDescending(orderItem => orderItem?.Amount).ToList()!)
                                        {
                                            if (item?.Id != null)
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

                        case 4:
                            Console.WriteLine("Enter the orderId of the orderitem that you want: ");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            Console.WriteLine("Enter the productId of the orderitem that you want: ");
                            int prodId2 = 0;
                            int.TryParse(Console.ReadLine(), out prodId2);
                            Console.WriteLine("Enter the new amount: ");
                            int Amount2 = 0;
                            int.TryParse(Console.ReadLine(), out Amount2);
                            OrderItems orderitems2 = new OrderItems();
                            orderitems2.OrderId = orderid2;
                            orderitems2.ProductId = prodId2;
                            orderitems2.Amount = Amount2;
                            orderitems2.Price = ((double)dal?.Product.Get(prodId2, 0).Price!) * Amount2;
                            dal?.OrderItem.Add(orderitems2);
                            dal?.OrderItem.Update(orderid2, prodId2);
                            break;

                        case 5:
                            Console.WriteLine("Enter the orderId of the orderitem that you want to delete: ");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            Console.WriteLine("Enter the productId of the orderitem that you want to delete: ");
                            int prodId3 = 0;
                            int.TryParse(Console.ReadLine(), out prodId3);
                            dal?.OrderItem.Delete(prodId3, orderid3);
                            break;
                        case 6:
                            Console.WriteLine("enter the OrderId: ");
                            int ordItemId = 0;
                            int.TryParse(Console.ReadLine(), out ordItemId);
                            Console.WriteLine("Enter the prodId: ");
                            int productItemId = 0;
                            int.TryParse(Console.ReadLine(), out productItemId);
                            Console.WriteLine("Enter the amount: ");
                            int ordItemAmount = 0;
                            int.TryParse(Console.ReadLine(), out ordItemAmount);
                            OrderItems orderItem = new OrderItems();
                            orderItem.OrderId = ordItemId;
                            orderItem.ProductId = productItemId;
                            orderItem.Amount = ordItemAmount;
                            orderItem.Price = ((double)dal?.Product.Get(productItemId, 0).Price!) * ordItemAmount;
                            Func<OrderItems?, bool>? func = oI => (oI?.OrderId == orderItem.OrderId && oI?.ProductId == orderItem.ProductId && oI?.Amount == orderItem.Amount && oI?.Price == orderItem.Price);
                            Console.WriteLine(dal?.OrderItem.GetItem(func).ToString());
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;

            }
        }
    }

    public static void Main(string[] args)
    {
        Display();
    }

}
