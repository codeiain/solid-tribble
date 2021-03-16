using System.Xml.Serialization;
using brady.Models.Output;

namespace brady.Models.Reference
{
    [XmlRoot(ElementName="Factors")]
    public class Factors {
        [XmlElement(ElementName="ValueFactor")]
        public ValueFactor ValueFactor { get; set; }
        [XmlElement(ElementName="EmissionsFactor")]
        public EmissionsFactor EmissionsFactor { get; set; }
    }
}