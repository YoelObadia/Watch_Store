using BlApi;
using DalApi;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace BlImplementation;

internal class BlCart : ICart
{
    static Random rand = new Random();
    int automaticOrderId = rand.Next(321, 400);
    int automaticOrderItemId = rand.Next(41, 100);// mnt
    DalApi.IDal? dal = DalApi.Factory.Get();


    /// <summary>
    /// Add a product to cart or add amount of the product
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="BO.StockNotValidExcpetion"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Add(BO.Cart cart, int productId)
    {
        lock (dal!)
        {

            BO.OrderItem orderItem= new BO.OrderItem();    
            BO.Product product = new BO.Product();
            DO.Products products = new DO.Products();
            products = (DO.Products)dal?.Product.Get(productId, 0)!;
            if (!cart.orderItems!.Exists(OrderItem => OrderItem!.ProductID == productId))// test if the product not exist in the Cart
            {

                if ((bool)dal?.Product.GetList().Contains(products)!)
                {

                    if (products.InStock == 0)
                            throw new BO.StockNotValidExcpetion("Empty stock!");// rupture de stock

                    else
                    {
                        product.ID = products.Id;
                        product.Name = products.Name;
                        product.Price = products.Price;
                        product.Category = (BO.Category)products.Category!;
                        product.InStock = products.InStock;
                        orderItem.ID= cart.orderItems.Count();
                        ++orderItem.ID;
                        orderItem.ProductID = productId;
                        orderItem.Name = product.Name;
                        orderItem.Price = product.Price;
                        orderItem.Amount = 1;
                        orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                        cart.TotalPrice += orderItem.TotalPrice;
                        cart.orderItems.Add(orderItem);
                    }
                }
                else
                {
                    throw new BO.NoExistingItemException();
                }
            }
            else
            {
                if (products.InStock == 0)
                    throw new BO.StockNotValidExcpetion("empty stock!");// rupture de stock
                else
                {
                    int index = cart.orderItems.FindIndex(OrderItem=> OrderItem!.ProductID == productId);
                    cart.orderItems[index]!.Amount += 1;
                    cart.orderItems[index]!.TotalPrice += products.Price;
                    cart.TotalPrice += cart.orderItems[index]!.TotalPrice;
                }
            }
            return cart;
        }
    }

    /// <summary>
    /// The client confirm his Cart into order
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Confirmation(BO.Cart cart)
    {
        lock (dal!)
        {

            var cart1=from item in cart.orderItems
                      where (dal?.Product.Get(item!.ProductID, 0).InStock - item.Amount) < 0
                      select item;
            if (cart1.Count()!=0)
            {
                throw new BO.NotEnoughInStockException("There is not enough stock of the product " + cart1.First().Name);
            }
            if (String.IsNullOrEmpty(cart.CustomerName) || String.IsNullOrEmpty(cart.CustomerAddress) || !cart.CustomerEmail!.Contains("@gmail.com"))
            {
                throw new BO.InvalidStringFormatException("Invalid details format");
            }
            if (cart.orderItems!.Count==0)
            {
                throw new BO.ItemNotExistInCartException("there are no product in the cart yet");
            }
            DO.Order order= new DO.Order();
            order.Id = automaticOrderId;
            order.CustomerAdress = cart.CustomerAddress;
            order.CustomerName=cart.CustomerName;
            order.CustomerEmail=cart.CustomerEmail;
            order.OrderDate=DateTime.Now;
            order.ShipDate = null;  // stage3 update
            order.DeliveryDate = null;  // stage3 update
            dal?.Order.Add(order);
            foreach (var item in cart.orderItems)
            {
                automaticOrderItemId = rand.Next(41, 100);
                DO.OrderItems orderItems= new DO.OrderItems();
                orderItems.Id = automaticOrderItemId;
                orderItems.OrderId = (int)dal?.Order.GetList().Last()?.Id!;// return the id of the last element that was added one the list
                orderItems.ProductId=item!.ProductID;
                orderItems.Amount = item.Amount;    
                orderItems.Price=item.TotalPrice;
                dal?.OrderItem.Add(orderItems);// add the new orderitem in the list of the data layer
                DO.Products products = new DO.Products();
                products = (DO.Products)dal?.Product.Get(item.ProductID, 0)!;
                products.InStock-=item.Amount;
                dal?.Product.Delete(products.Id, 0);// update the new amount of the prod after the confirmation of the order
                dal?.Product.Add(products);//////////
            }
            return cart;
        }
    }

    /// <summary>
    /// Update the amount of Product in the Cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Update(BO.Cart cart, int productId, int newAmount)
    {

        lock (dal!)
        {

            if (newAmount<0)
            {
                throw new BO.NegativeAmountException("Cannot choose a negative amount");
            }
            if ((bool)!dal?.Product.GetList().ToList().Exists(Product=> Product?.Id==productId)!)
            {
                throw new BO.NoExistingItemException("Product Not Exist");
            }
            if(cart.orderItems!.Exists(OrderItem => OrderItem!.ProductID == productId))
            {
                int index=cart.orderItems.FindIndex(OrderItem=> OrderItem!.ProductID==productId);
                if (cart.orderItems[index]!.Amount!=newAmount)
                {
                    if (cart.orderItems[index]!.Amount<newAmount)
                    {
                        for (int i = cart.orderItems[index]!.Amount; i < newAmount; i++)
                        {
                            Add(cart, productId);
                        }
                    }
                    if (cart.orderItems[index]!.Amount>newAmount && cart.orderItems[index]!.Amount!=0)
                    {
                        double price = (double)dal?.Product.Get(productId, 0).Price!;
                        int dif = cart.orderItems[index]!.Amount - newAmount;
                        cart.orderItems[index]!.Amount = newAmount;
                        cart.orderItems[index]!.TotalPrice = newAmount * price;
                        cart.TotalPrice -= dif * price;
                    }
                    if (newAmount==0)
                    {
                        double difprice = cart.orderItems[index]!.TotalPrice;
                        cart.orderItems.RemoveAt(index);
                        cart.TotalPrice-= difprice;
                    }
                }
                return cart;
            }
            else
            {
                throw new BO.ItemNotExistInCartException("Product not exist in your cart");
            }
        }
    }
}
