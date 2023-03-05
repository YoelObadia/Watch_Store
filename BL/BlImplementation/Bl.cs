using BlApi;
using BO;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    private static Bl instance = null;
    private static readonly object padlock=new object();
    public static Bl Instance 
    { 
        get 
        {
            if (instance==null)
            {
                lock (padlock)
                {
                    if (instance==null)
                    {
                        instance = new Bl();
                    }
                }

            }
            return instance; 
        }
    }
    public IProduct Product => new BlProduct();
    public IOrder Order => new BlOrder();
    public ICart Cart => new BlCart();
}