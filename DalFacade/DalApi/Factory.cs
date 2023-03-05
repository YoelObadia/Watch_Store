namespace DalApi;
using System.Reflection;
using static DalApi.DalConfig;

/// <summary>
/// Allow use to use Singleton to create one reference only for every entity when we use the dal functions
/// </summary>
public static class Factory
{
    public static IDal? Get()
    {
        
        string dalType = "xml"// the same name a we can found on the dal.config.xml file
            ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
        // we access to the element on the dictionnary wich xml is his key
        // s_dalPackage(the dictionnary) is defined in the dalConfig file
        string dal = s_dalPackages[dalType]
           ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");
        try
        {
            Assembly.Load(dal ?? throw new DalConfigException($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }

        Type? type = Type.GetType($"Dal.{dal}, {dal}")
            ?? throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
    

}
