
using System.Xml.Linq;

namespace DO;

public struct OrderItems
{
    public int Id { get; set; }
    public int ProductId { get; set; }  
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    OrderItem Id= {Id}
    Order ID = {OrderId} 
    Product ID={ProductId}
    price - {Price}
    Amount: {Amount}
    ";
}
