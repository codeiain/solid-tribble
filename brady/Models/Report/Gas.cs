using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="Gas")]
    public class Gas {
        [XmlElement(ElementName="GasGenerator")]
        public List<GasGenerator> GasGenerator { get; set; }
    }
}