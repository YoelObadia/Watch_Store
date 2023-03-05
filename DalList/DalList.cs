using DalApi;


namespace Dal;

sealed internal class DalList : IDal
{
    internal static DalList? instance = null;

    private static readonly object padlock = new object();

    internal static DalList Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DalList();
                }
                return instance;
            }
        }
    }

    private DalList() { }
    public IProduct Product => new DalProducts();

    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem => new DalOrderItems();
}


