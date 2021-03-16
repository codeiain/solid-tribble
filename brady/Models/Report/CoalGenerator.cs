using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="CoalGenerator")]
    public class CoalGenerator : IFuel
    {
        [XmlElement(ElementName="Name")]
        public string Name { get; set; }
        [XmlElement(ElementName="Generation")]
        public Generation Generation { get; set; }
        [XmlElement(ElementName="TotalHeatInput")]
        public string TotalHeatInput { get; set; }
        [XmlElement(ElementName="ActualNetGeneration")]
        public string ActualNetGeneration { get; set; }
        [XmlElement(ElementName="EmissionsRating")]
        public string EmissionsRating { get; set; }
    }
}