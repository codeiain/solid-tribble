using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="Coal")]
    public class Coal {
        [XmlElement(ElementName="CoalGenerator")]
        public List<CoalGenerator> CoalGenerator { get; set; }
    }
}