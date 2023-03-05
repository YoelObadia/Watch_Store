using DO;

namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
     OrderItem ID = {ID}
     Name={Name}
     ProductId ={ProductID},
     Price - {Price}
     Amount: {Amount}
     Total price - {TotalPrice}
    ";
}
