using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot("EmissionsFactor")]
    public class EmissionsFactor
    {
        public double High { get; }
        public double Medium { get; }
        public double Low { get; }
    }
}