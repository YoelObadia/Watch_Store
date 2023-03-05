using BO;
namespace BlApi;

/// <summary>
/// /////////////////////////////IBoCart Interface////////////////////////////
/// </summary>
public interface ICart
{
    public Cart Add(Cart cart, int productId);
    public Cart Update(Cart cart, int productId, int newAmount);
    public Cart Confirmation(Cart cart);
}
