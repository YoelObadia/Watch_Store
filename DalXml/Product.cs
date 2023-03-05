using DAL;
using DalApi;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace Dal;
internal class Product : IProduct
{
    XElement ProductRoot;

    public string ProductPath;
    public Product()
    {
        string localPath;
        string str = Assembly.GetExecutingAssembly().Location;
        localPath = Path.GetDirectoryName(str)!;
        localPath = Path.GetDirectoryName(localPath)!;

        localPath += @"\xml";
        string extProductPath = localPath + @"\ProductXml.xml";

        // Verify if the file exists or not and create him if he doesn't exist
        if (!File.Exists(extProductPath))
        {
            HelpXml.CreateFiles(extProductPath);
        }
        else
        {
            HelpXml.LoadData(extProductPath);
        }
        ProductPath = extProductPath;
    }

    /// <summary>
    /// Add a product to the list of products in the xml file
    /// </summary>
    /// <param name="t"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(DO.Products t)
    {
        XElement id = new XElement("Id", t.Id);
        XElement name = new XElement("Name", t.Name);
        XElement price = new XElement("Price", t.Price);
        XElement category = new XElement("Category", t.Category);
        XElement instok = new XElement("InStock", t.InStock);

        ProductRoot.Add(new XElement("Products",id,name,price,category,instok));
        ProductRoot.Save(ProductPath);
    }

    /// <summary>
    /// Remove a specified product from the list of pproducts in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int Id1, int Id2)
    {
        ProductRoot = XElement.Load(ProductPath);
        try
        {
            XElement product = (from item in ProductRoot.Elements()
                                where int.Parse(item.Element("Id")!.Value) == Id1
                                select item).FirstOrDefault()!;
            product.Remove();
            ProductRoot.Save(ProductPath);
        }
        catch
        {
            throw new Exception("Impossible to delete the product");
        }
    }

    /// <summary>
    /// Return a specified product from the list of products in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Products Get(int Id1, int Id2)
    {
        ProductRoot = XElement.Load(ProductPath);
        DO.Products product = new();
        try
        {
            ProductRoot = XElement.Load(ProductPath);

            // LINQ to XML to get the list of products. The type of products is IEnumerable<XElement>?
            var products = from item in ProductRoot.Elements()
                           select item;


            List<DO.Products?> p = new List<DO.Products?>();

            // Allow us to cast from IEnumerable<XElement>? to List<Products> that is also an IEnumerable<Products?> 
            foreach (var item1 in products)
            {
                product.Id = int.Parse(item1.Element("Id")!.Value);
                product.Name = item1.Element("Name")!.Value;
                product.Price = double.Parse(item1.Element("Price")!.Value);
                product.Category = (DO.Category)Enum.Parse(typeof(DO.Category), item1.Element("Category")!.Value);
                product.InStock = int.Parse(item1.Element("InStock")!.Value);
                p.Add(product);
                // Reinitialize the product for next iteration 
                product = new();
            }
            var prod = from item1 in p
                       where item1?.Id == Id1
                       select item1;
            DO.Products products1=new();
            products1 = (DO.Products)prod.First()!;
            return products1;
        }
        catch
        {
            throw new Exception("Product doen't exists!");
        }
    }

    /// <summary>
    /// Return the list of products in the xml file
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Products?> GetList(Func<DO.Products?, bool>? predicate = null)
    {
        // Load the file in the root
        ProductRoot = XElement.Load(ProductPath);

        // LINQ to XML to get the list of products. The type of products is IEnumerable<XElement>?
        var products = from item in ProductRoot.Elements()
                       select item;


        List<DO.Products?> p = new List<DO.Products?>();
        DO.Products product=new();

        // Allow us to cast from IEnumerable<XElement>? to List<Products> that is also an IEnumerable<Products?> 
        foreach (var item1 in products)
        {
            product.Id = int.Parse(item1.Element("Id")!.Value);
            product.Name = item1.Element("Name")!.Value;
            product.Price = double.Parse(item1.Element("Price")!.Value);
            product.Category = (DO.Category)Enum.Parse(typeof(DO.Category), item1.Element("Category")!.Value);
            product.InStock = int.Parse(item1.Element("InStock")!.Value);
            p.Add(product);
            // Reinitialize the product for next iteration 
            product = new();
        }
        if (predicate != null)
        {
            p = (List<DO.Products?>)p.Where(predicate);
        }
        return p;
    }

    /// <summary>
    /// Update a specified product in the list of products in the xml file
    /// </summary>
    /// <param name="Id1"></param>
    /// <param name="Id2"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int Id1, int Id2)
    {
        // Load the file in the root
        ProductRoot = XElement.Load(ProductPath);
        //We get the target product
        XElement product = (from item in ProductRoot.Elements()
                            where int.Parse(item.Element("Id")!.Value) == Id1
                            select item).FirstOrDefault()!;
        // We get the new product
        XElement product1 = (from item in ProductRoot.Elements()
                             where int.Parse(item.Element("Id")!.Value) == Id1
                             select item).Last();
        // We update the target product with the values that the admin entered in the PL
        foreach (var item in ProductRoot.Elements())
        {
            if (item == product)
            {
              product.Element("Price")!.SetValue(double.Parse(product1.Element("Price")!.Value));
              product.Element("InStock")!.SetValue(int.Parse(product1.Element("InStock")!.Value));
              product.Element("Category")!.SetValue((DO.Category)Enum.Parse(typeof(DO.Category),product1.Element("Category")!.Value));
              product.Element("Name")!.SetValue(product1.Element("Name")!.Value);
            }
        }
        product1.Remove();
        ProductRoot.Save(ProductPath);
    }
}