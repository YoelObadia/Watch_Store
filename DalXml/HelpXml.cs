using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL;

class HelpXml
{
    static string Path = @"..\..\..\..\xml\";
    static XElement Root;

    /// <summary>
    /// File initialilize
    /// </summary>
    static HelpXml()
    {
        if (!Directory.Exists(Path))
            Directory.CreateDirectory(Path);
    }

    /// <summary>
    /// Generic function to save a list in the XML file that match the given path using XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="filePath"></param>
    public static void SaveListToXmlSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new(filePath, FileMode.Create);
            XmlSerializer x = new(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch
        {
            throw new Exception("Failed to create xml file");
        }
    }

    /// <summary>
    /// Generic function that returns a list of items from the file that match the given path using XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <returns> List of items located in the wanted XML file</returns>
    public static List<T> LoadListFromXmlSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<T> list;
                XmlSerializer x = new(typeof(List<T>));
                FileStream file = new(filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file)!;
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch
        {
            throw new Exception("Failed to load xml file");
        }
    }

    /// <summary>
    /// Return XElement type in the XML file located in the given path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>XElement</returns>
    public static XElement LoadData(string filePath)
    {
        try
        {
            return XElement.Load(filePath);
        }
        catch 
        { 
            throw new Exception("Failed to load xml file");
        }
    }

    /// <summary>
    /// Create XML file with the given path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>XElement</returns>
    public static XElement CreateFiles(string filePath)
    {
        string rootName = filePath.Split(".")[0];
        Root = new XElement(rootName);
        Root.Save(Path + filePath);
        return Root;
    }
}