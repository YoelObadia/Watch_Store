
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// struct for product
/// </summary>
public struct Products
{
    /// <summary>
    /// unique id for each product
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// name of the product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// cathegorie of the product
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// amount of the product in the stock
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
    Product ID= {Id}: {Name},
    category - {Category}
    Price: {Price}
    Amount in stock: {InStock}
    ";
}
