using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="Wind")]
    public class Wind {
        [XmlElement(ElementName="WindGenerator")]
        public List<WindGenerator> WindGenerator { get; set; }
    }
}