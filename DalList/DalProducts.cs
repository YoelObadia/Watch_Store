using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;
internal class DalProducts : IProduct
{

    /// <summary>
    /// Call the function Add in the DataSource to add a new product in the list of products
    /// </summary>
    /// <param name="products"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(Products products)
    {
        DataSource.GetAddProductToList(products);
    }

    /// <summary>
    /// Return a specified product from the list of products
    /// </summary>
    /// <param name="ProductId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Products Get(int ProductId,int v=0)
    {
        Products products = new Products();
        var products1 = from item in DataSource._products
                        where item?.Id == ProductId
                        select item;
        products = (Products)products1.First()!; // products1 is an enumerable that contains just one product
        return products!;
    }

    /// <summary>
    /// Return a list of products from the DataSource
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Products?> GetList(Func<Products?, bool>? predicate=null)
    {
        if (predicate!=null)
        {
            var newList = DataSource._products.Where(predicate); // newList selects just the products that who pass the condition
            return newList;
        }
        return DataSource._products;
    }

    /// <summary>
    /// Remove a specified product from the list of products
    /// </summary>
    /// <param name="ProductId"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ProductId, int v = 0)
    {
        DataSource._products.Remove(Get(ProductId));
    }

    /// <summary>
    /// Update a specified product in the list of products
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int productId, int v = 0)
    {
        int count = 0;
        var products = from item in DataSource._products
                       where item?.Id == productId
                       select item;  // products is an enumarable that contains the old product and the updated product
            
        count = DataSource._products.FindIndex(p => p.Equals(products.First()));
        DataSource._products[count] = DataSource._products.Last();
        DataSource._products.RemoveAt(DataSource._products.Count()-1);
        return;
        throw new Exception("Product cannot be found!");

    }
}