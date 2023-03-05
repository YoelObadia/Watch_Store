using DAL;
using DalApi;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dal;

internal class Order : IOrder
{
    public string OrderPath;
    public Order()
    {
        string localPath;
        string str = Assembly.GetExecutingAssembly().Location;
        localPath = Path.GetDirectoryName(str)!;
        localPath = Path.GetDirectoryName(localPath)!;

        localPath += @"\xml";
        string extOrderPath = localPath + @"\OrderXml.xml";

        // Verify if the file exists or not and create him if he doesn't exist
        if (!File.Exists(extOrderPath))
        {
            HelpXml.CreateFiles(extOrderPath);
        }
        else
        {
            HelpXml.LoadData(extOrderPath);
        }
        OrderPath = extOrderPath;
    }

    /// <summary>
    /// Add an order to the xml file at the end of the list that already exists
    /// </summary>
    /// <param name="t"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(DO.Order t)
    {
        List<DO.Order> ListOrders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        if (ListOrders.Any(c => c.Id == t.Id))
            throw new Exception($"The customer ID {t.Id} exists already in the data!!");
        ListOrders.Add(t);
        HelpXml.SaveListToXmlSerializer(ListOrders, OrderPath);
    }

    /// <summary>
    /// Remove a specified order in the xml file 
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int Id1, int Id2)
    {
        List<DO.Order> orders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        DO.Order order = (from item in orders
                          where item.Id == Id1
                          select item).FirstOrDefault();
        orders.Remove(order);
        HelpXml.SaveListToXmlSerializer(orders, OrderPath);
    }

    /// <summary>
    /// Return a specified oroder from the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(int Id1, int Id2)
    {
        HelpXml.LoadData(OrderPath);
        DO.Order? order;
        IEnumerable<DO.Order?> orders = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath);
        try
        {
            order = (from item in orders
                     where item?.Id == Id1
                     select item).FirstOrDefault();
            return (DO.Order)order!;
        }
        catch
        {
            throw new Exception("Order doesn't exist!");
        }
    }

    /// <summary>
    /// Return the list of orders in the the xml file
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? predicate = null)
    {
        var listOrders = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath);
        if (predicate!=null)
        {
            var listOrders1 = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath).Where(predicate!);
            return listOrders1;
        }
        return listOrders;
    }

    /// <summary>
    /// Update a specified order in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int Id1, int Id2)
    {
        List<DO.Order> listOrders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        DO.Order order = (from item in listOrders
                          where item.Id == Id1
                          select item).FirstOrDefault();
        Console.WriteLine("Enter the Customer Name: ");
        string name = Console.ReadLine()!;
        Console.WriteLine("Enter customer Email: ");
        string Email = Console.ReadLine()!;
        Console.WriteLine("Enter Customer adress: ");
        string Adress = Console.ReadLine()!;
        DO.Order newOrder = new() { Id = Id1 , CustomerName = name, CustomerEmail = Email, CustomerAdress = Adress, OrderDate = order.OrderDate, ShipDate = order.ShipDate, DeliveryDate = order.DeliveryDate};
        order = newOrder;
        HelpXml.SaveListToXmlSerializer(listOrders, OrderPath);
    }
}
