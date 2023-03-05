using BO;
namespace BlApi;
/// <summary>
/// ////////////////////IBoProduct Interface////////////////////////
/// </summary>
public interface IProduct
{
    public IEnumerable<ProductForList?> GetProductForLists(Func<ProductForList?, bool>? func = null);
    public Product GetDirector(int productId);
    public ProductItem GetClient(int productId, Cart cart);
    public void Add(Product product);
    public void Delete(int productId);
    public void Update(Product product);
}