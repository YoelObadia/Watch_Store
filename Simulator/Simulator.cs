using System.Reflection.Metadata.Ecma335;

namespace Simulator;

public static class Simulator
{
    static BlApi.IBl bl = BlApi.Factory.Get();
    private static volatile bool _isRunning;

    /// <summary>
    /// delegate event for complate Simulator
    /// </summary>
    public delegate void SimulationCompleteEventHandler();
    public static event SimulationCompleteEventHandler OnSimulationComplete;

    /// <summary>
    /// delegate event for Update Simulator
    /// </summary>
    public delegate void UpdateEventHandler(BO.Order? order, DateTime newTime, int delay);
    public static event UpdateEventHandler OnUpdate;


    public static bool isAlreadyOpen = false;
    public static Random rnd = new Random();
    public static BO.Order? order = new BO.Order();
    /// <summary>
    /// Start simulation, open the thread and The work
    /// </summary>
    public static void StartSimulation()
    {
        ///open the thread
        new Thread(() =>
        {
            _isRunning = true;
            ///do the work until the flag changed
            while (_isRunning)
            {
                work();
                Thread.Sleep(1000);
            }
            OnSimulationComplete();
        }).Start();
    }
    /// <summary>
    /// The work is Each Second it Take organ from the Data and move action to the BL and PL
    /// </summary>
    private static void work()
    {

        DateTime newDate = new DateTime();
        order = bl.Order?.GetOrderItem(bl.Order.GetOldestOrder());
        if (order != null)
        {
            int Delay = rnd.Next(3, 11);
            newDate = DateTime.Now + new TimeSpan(0, 0, Delay);
            ///call the func that rigister to Update
            OnUpdate(order, newDate, Delay);
            Thread.Sleep(Delay * 1000);
            ///update the time in BL 
            if (order.Status == BO.OrderStatus.Expedited)
            {
                bl.Order?.UpdadteOrderReceived(order.Id);
            }
            else if (order.Status == BO.OrderStatus.Approved)
            {
                bl.Order?.UpdateOrderShipping(order.Id);
            }
        }
        else
        /// ///call the func that rigister to Update by null
        {
            OnUpdate(null, newDate, 0);
        }

    }
    /// <summary>
    /// StopSimulation
    /// </summary>
    public static void StopSimulation()
    {
        _isRunning = false;

    }
    /// <summary>
    /// Register to complate event
    /// </summary>
    /// <param name="handler"></param>
    public static void RegisterForSimulationCompleteEvent(SimulationCompleteEventHandler handler)
    {
        OnSimulationComplete += handler;
    }
    /// <summary>
    /// UNRegister to complate event
    /// </summary>
    /// <param name="handler"></param>
    public static void UnregisterFromSimulationCompleteEvent(SimulationCompleteEventHandler handler)
    {
        OnSimulationComplete -= handler;
    }

    /// <summary>
    /// Register to Update event
    /// </summary>
    /// <param name="handler"></param>
    public static void RegisterForUpdateEvent(UpdateEventHandler handler)
    {
        OnUpdate += handler;
    }
    /// <summary>
    /// UnRegister to Update event
    /// </summary>
    public static void UnregisterFromUpdateEvent(UpdateEventHandler handler)
    {
        OnUpdate -= handler;
    }
}