
using BO;

namespace BlApi;

/// <summary>
/// ////////////////////////IBoOrder Interface///////////////////////
/// </summary>
public interface IOrder
{
    public IEnumerable<BO.OrderForList?> GetOrderList(Func<OrderForList?, bool>? func = null);
    public BO.Order GetOrderItem(int OrderId);
    public BO.Order UpdateOrderShipping(int OrderId);
    public BO.Order UpdadteOrderReceived(int OrderId);
    public BO.OrderTracking TrackingOrder(int OrderId);
    public int GetOldestOrder();
    public void DeletOrderForAdmin(int OrderId);
}
