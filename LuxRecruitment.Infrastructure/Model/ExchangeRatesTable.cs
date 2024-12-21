using System.Xml.Serialization;

namespace LuxRecruitment.Infrastructure.Model
{
    public sealed class ExchangeRatesTable
    {
        [XmlElement("Table")]
        public string Table { get; set; }

        [XmlElement("No")]
        public string No { get; set; }

        [XmlElement("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [XmlArray("Rates")]
        [XmlArrayItem("Rate")]
        public List<Rate> Rates { get; set; }
    }
}
