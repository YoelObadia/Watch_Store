using DO;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus? Status { get; set; }

    public List<Tuple<DateTime?, string?>?>? OrderTrackingList = new List<Tuple<DateTime?, string?>?>();


    public override string ToString() => $@"
     Order ID: {ID}
     Order Status: {Status}
     {OrderTrackingList[0].Item1}->{OrderTrackingList[0].Item2}
     {OrderTrackingList[1].Item1}->{OrderTrackingList[1].Item2}
     {OrderTrackingList[2].Item1}->{OrderTrackingList[2].Item2}
     "
    ;
}
