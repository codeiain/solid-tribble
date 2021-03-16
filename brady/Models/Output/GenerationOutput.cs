using System.Collections.Generic;
using System.Xml.Serialization;

namespace brady.Models.Output
{
    [XmlRoot(ElementName="GenerationOutput")]
    public class GenerationOutput {
        [XmlElement(ElementName="Totals")]
        public Totals Totals { get; set; }
        [XmlElement(ElementName="MaxEmissionGenerators")]
        public MaxEmissionGenerators MaxEmissionGenerators { get; set; }
        [XmlElement(ElementName="ActualHeatRates")]
        public List<ActualHeatRates> ActualHeatRates { get; set; }

        public GenerationOutput() {
            Totals = new Totals();
            ActualHeatRates = new List<ActualHeatRates>();
            MaxEmissionGenerators = new MaxEmissionGenerators();
        }
    }
}