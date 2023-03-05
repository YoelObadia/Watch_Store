using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;
internal class DalOrderItems : IOrderItem
{
    /// <summary>
    /// Call the function Add in the DataSource to add a new orderitem in the list of orderitems
    /// </summary>
    /// <param name="orderItems"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(OrderItems orderItems)
    {
        DataSource.GetAddOrderItemToList(orderItems);
    }

    /// <summary>
    /// Return a specified orderitem from the list of orderitem
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="ProductId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItems Get(int OrderId, int ProductId)
    {
        OrderItems orderItems = new OrderItems();
        var orderItems1 = from item in DataSource._orderItems
                          where item?.OrderId == OrderId && item?.ProductId == ProductId
                          select item;
        orderItems = (OrderItems)orderItems1.First()!; // orderItems1 is an enumarable that contains just one orderItem
        return orderItems!;
    }

    /// <summary>
    /// Return a list of orderitems from the DataSource
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItems?> GetList(Func<OrderItems?, bool> func=null!)
    {
        if (func != null)
        {
            var newList = DataSource._orderItems.Where(func); // newList selects just the orderItems who pass the condition
            return newList;
        }
        return DataSource._orderItems;
    }

    /// <summary>
    /// Remove a specified orderitem from the list of orderitems
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="orderId"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ProductId, int orderId)
    {
        DataSource._orderItems.Remove(Get(orderId, ProductId));
    }

    /// <summary>
    /// Update a specified orderitem in the list of orderitems
    /// </summary>
    /// <param name="orderId"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int orderId, int productId)
    {
        int count = 0;
        var orderItems1 = from item in DataSource._orderItems
                          where item?.OrderId == orderId && item?.ProductId == productId
                          select item;
            
            count = DataSource._orderItems.FindIndex(oI => oI?.OrderId == orderItems1.First()?.OrderId && oI?.ProductId == orderItems1.First()?.ProductId);
            int id = (int)orderItems1.First()?.Id!;
            DataSource._orderItems[count] = DataSource._orderItems.Last();
            OrderItems oi = (OrderItems)DataSource._orderItems[count]!; // Keep the same orderItem Id after the swap
            oi.Id = id; // Keep the same orderItem Id after the swap
            DataSource._orderItems[count] = oi; // Keep the same orderItem Id after the swap
            DataSource._orderItems.RemoveAt(DataSource._orderItems.Count() - 1); 
            return;
        throw new Exception("OrderItem cannot be found!");

    }
}