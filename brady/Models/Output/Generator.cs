using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot(ElementName = "Generator")]
    public class Generator
    {
        [XmlElement(ElementName = "Name")] public string Name { get; set; }
        [XmlElement(ElementName = "Total")] public string Total { get; set; }
    }
}