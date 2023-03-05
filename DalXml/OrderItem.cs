using DAL;
using DalApi;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dal;

internal class OrderItem : IOrderItem
{
    public string OrderItemPath;

    public OrderItem()
    {
        string localPath;
        string str = Assembly.GetExecutingAssembly().Location;
        localPath = Path.GetDirectoryName(str)!;
        localPath = Path.GetDirectoryName(localPath)!;

        localPath += @"\xml";
        string extOrderItemPath = localPath + @"\OrderItemXml.xml";

        // Verify if the file exists or not and create him if he doesn't exist
        if (!File.Exists(extOrderItemPath))
        {
            HelpXml.CreateFiles(extOrderItemPath);
        }
        else
        {
            HelpXml.LoadData(extOrderItemPath);
        }
        OrderItemPath = extOrderItemPath;
    }

    /// <summary>
    /// Add a orderitem at the end of the list in the xml file
    /// </summary>
    /// <param name="t"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(DO.OrderItems t)
    {
        List<DO.OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems>(OrderItemPath);
        if (ListOrderItems.Any(c => c.Id == t.Id))
            throw new Exception("Order item already exists!");
        ListOrderItems.Add(t);
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }

    /// <summary>
    /// Remove a orderitem from the list of products in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int Id1, int Id2)
    {
        List<DO.OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems>(OrderItemPath);
        DO.OrderItems orderItem = (from item in ListOrderItems
                                   where item.ProductId == Id1 && item.OrderId == Id2
                                   select item).FirstOrDefault();
        ListOrderItems.Remove(orderItem);
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }

    /// <summary>
    /// Return a specified orderitem
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItems Get(int Id1, int Id2)
    {
        List<DO.OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems>(OrderItemPath);
        try
        {
            DO.OrderItems orderItem = (from item in ListOrderItems
                                       where item.OrderId == Id1 && item.ProductId == Id2
                                       select item).FirstOrDefault();

            return orderItem;
        }
        catch
        {
            throw new Exception("Order item doesn't exists");
        }
        
    }

    /// <summary>
    /// Return the list of orderitem in the xml file
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItems?> GetList(Func<DO.OrderItems?, bool>? predicate = null)
    {
        var ListOrderItems1 = HelpXml.LoadListFromXmlSerializer<DO.OrderItems?>(OrderItemPath);
        if (predicate!=null)
        {
             var ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems?>(OrderItemPath).Where(predicate!);
            return ListOrderItems1;
        }
        return ListOrderItems1;
    }

    /// <summary>
    /// Update a specified orderitem in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int Id1, int Id2)
    {
        List<DO.OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems>(OrderItemPath);
        DO.OrderItems orderItem = (from item in ListOrderItems
                                   where item.OrderId == Id1 && item.ProductId == Id2
                                   select item).FirstOrDefault();
        Console.WriteLine("Enter the orderId of the orderitem that you want: ");
        int orderId = 0;
        int.TryParse(Console.ReadLine(), out orderId);
        Console.WriteLine("Enter the productId of the orderitem that you want: ");
        int productId = 0;
        int.TryParse(Console.ReadLine(), out productId);
        Console.WriteLine("Enter the new amount: ");
        int Amount = 0;
        int.TryParse(Console.ReadLine(), out Amount);
        DO.OrderItems newOrderItem = new() { Id = orderItem.Id, OrderId= orderId, ProductId = productId, Amount = Amount, Price = ((double)Get(productId, 0).Price!) * Amount };
        orderItem = newOrderItem;
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }
}
