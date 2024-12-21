using System.Xml.Serialization;

namespace LuxRecruitment.Infrastructure.Model
{
    public sealed class Rate
    {
        [XmlElement("Currency")]
        public string Currency { get; set; }

        [XmlElement("Code")]
        public string Code { get; set; }

        [XmlElement("Mid")]
        public decimal Mid { get; set; }
    }
}
