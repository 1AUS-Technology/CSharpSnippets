using System.Xml.Serialization;

namespace HeadFirstDesignPatterns.StructuralPatterns.Adapter;

public class AdapterRunner
{
    public static void Run()
    {
        var dictionaryContainerClassAsXml = new DictionaryContainerClassAsXml();
        var xmlSerializer = new XmlSerializer(typeof(DictionaryContainerClassAsXml));
        xmlSerializer.Serialize(Console.Out, dictionaryContainerClassAsXml);


    }
}