using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot(ElementName="Totals")]
    public class Totals {
        [XmlElement(ElementName="Generator")]
        public List<Generator> Generator { get; set; }

        public Totals()
        {
            Generator = new List<Generator>();
        }
    }
}