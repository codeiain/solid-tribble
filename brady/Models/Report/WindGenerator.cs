using System.Xml.Serialization;

namespace brady.Models.Report
{
    [XmlRoot(ElementName="WindGenerator")]
    public class WindGenerator:IFuel {
        [XmlElement(ElementName="Name")]
        public string Name { get; set; }
        [XmlElement(ElementName="Generation")]
        public Generation Generation { get; set; }

        [XmlIgnore]
        public string EmissionsRating
        {
            get => "0";
            set { }
        }

        [XmlElement(ElementName="Location")]
        public string Location { get; set; }
    }
}