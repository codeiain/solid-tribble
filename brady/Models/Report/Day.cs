using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="Day")]
    public class Day {
        [XmlElement(ElementName="Date")]
        public string Date { get; set; }
        [XmlElement(ElementName="Energy")]
        public string Energy { get; set; }
        [XmlElement(ElementName="Price")]
        public string Price { get; set; }
    }
}