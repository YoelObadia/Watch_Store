using DalApi;

using DO;
using System.Data.Common;

namespace Dal;
sealed internal class DalXml : IDal
{
    // =null that if we dont need to create a "new bl" it will not create it
    private static DalXml instance = null;
    // for safty. So that if requests come from two places at the same time, it will not create it twice 
    private static readonly object padlock = new object();

    public static DalXml Instance
    {
        get
        {
            //if "instance" hasn`t yet been created, a new one will be created 
            if (instance == null)
            {
                //stops a request from two places at the same time
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DalXml();
                    }
                }
            }
            return instance;
        }
    }
    
    public IProduct Product { get; } = new Product();
    public IOrder Order { get; } = new Order();
    public IOrderItem OrderItem { get; } = new OrderItem();
}
