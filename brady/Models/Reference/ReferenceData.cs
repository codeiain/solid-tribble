using System.Xml.Serialization;

namespace brady.Models.Reference
{
    [XmlRoot(ElementName="ReferenceData")]
    public class ReferenceData {
        [XmlElement(ElementName="Factors")]
        public Factors Factors { get; set; }
    }
}