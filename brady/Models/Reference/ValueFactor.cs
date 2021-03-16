using System.Xml.Serialization;

namespace brady.Models.Reference
{
    [XmlRoot(ElementName="ValueFactor")]
    public class ValueFactor {
        [XmlElement(ElementName="High")]
        public string High { get; set; }
        [XmlElement(ElementName="Medium")]
        public string Medium { get; set; }
        [XmlElement(ElementName="Low")]
        public string Low { get; set; }
    }
}