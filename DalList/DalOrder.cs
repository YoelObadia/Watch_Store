using DO;
using DalApi;
using System.Dynamic;
using System;
using System.Runtime.CompilerServices;

namespace Dal;
internal class DalOrder:IOrder
{
    /// <summary>
    /// Call the function Add in the DataSource to add a new order in the list of orders
    /// </summary>
    /// <param name="order"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(Order order) 
    {
        DataSource.GetAddOrderToList(order);
    }

    /// <summary>
    /// Return a specified order
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int OrderId, int val=0)
    {
        Order order = new Order();
        var order1 = from item in DataSource._orders
                     where item?.Id == OrderId
                     select item;
        order = (Order)order1.First()!; //order1 is an enumerable that contains just one order
        return order;
    }

    /// <summary>
    /// Return a list of orders in the DataSource
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetList(Func<Order?,bool> func = null!)
    {
        if (func != null)
        {
            var newList = DataSource._orders.Where(func); // newList selects just the orders who pass the condition
            return newList;
        }
        return DataSource._orders;
    }

    /// <summary>
    /// Remove from the list of orders a specified order
    /// </summary>
    /// <param name="orderId"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int orderId,int v=0)
    {
        DataSource._orders.Remove(Get(orderId));
    }

    /// <summary>Update a specified order in the list of orders
    /// </summary>
    /// <param name="orderId"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int orderId,int v=0)
    {
        int count = 0;
        var newOrder = from item in DataSource._orders
                       where item?.Id == orderId
                       select item;
        count = DataSource._orders.FindIndex(o => o?.CustomerName == newOrder.First()?.CustomerName && o?.CustomerEmail == newOrder.First()?.CustomerEmail && o?.CustomerAdress == newOrder.First()?.CustomerAdress);
        DataSource._orders[count] = DataSource._orders.Last();
        DataSource._orders.RemoveAt(DataSource._orders.Count() - 1);
        return;
        throw new Exception("Order cannot be found!");
    }
}
