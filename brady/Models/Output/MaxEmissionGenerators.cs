using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot(ElementName="MaxEmissionGenerators")]
    public class MaxEmissionGenerators {
        [XmlElement(ElementName="Day")]
        public List<Day> Day { get; set; }

        public MaxEmissionGenerators()
        {
            Day = new List<Day>();
        }
    }
}