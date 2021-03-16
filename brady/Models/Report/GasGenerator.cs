using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName = "GasGenerator")]
    public class GasGenerator : IFuel
    {
        [XmlElement(ElementName = "Name")] public string Name { get; set; }

        [XmlElement(ElementName = "Generation")]
        public Generation Generation { get; set; }

        [XmlElement(ElementName = "EmissionsRating")]
        public string EmissionsRating { get; set; }
    }
}