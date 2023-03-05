using DO;

namespace BO;

public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public List<OrderItem?>? orderItems=new List<OrderItem?>();
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string OdrerItems = string.Join("", orderItems!);
        return ($@"
        
        Order Id: {Id}
        Customer Name: {CustomerName}
        CustomerEmail: {CustomerEmail}
        CustomerAdress: {CustomerAdress}
        Order Date: {OrderDate}
        Order Status: {Status}
        Payment Date: {PaymentDate}
        Ship Date: {ShipDate}
        DeliveryDate: {DeliveryDate}
        Order Item: {OdrerItems}
        Toal Price: {TotalPrice}
        ");

    }
}
