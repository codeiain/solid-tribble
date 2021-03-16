using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot(ElementName="ActualHeatRates")]
    public class ActualHeatRates {
        [XmlElement(ElementName="Name")]
        public string Name { get; set; }
        [XmlElement(ElementName="HeatRate")]
        public string HeatRate { get; set; }
    }
}