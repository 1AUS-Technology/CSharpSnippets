using System.Xml.Serialization;

namespace HeadFirstDesignPatterns.CreationPatterns.Prototype;

public static class DeepCopyExtensions
{
    public static T? DeepCopyXml<T>(this T obj) where T : class
    {
        using var ms = new MemoryStream();
        XmlSerializer s = new XmlSerializer(typeof(T));
        s.Serialize(ms, obj);
        ms.Position = 0;
        return s.Deserialize(ms) as T;
    }
}