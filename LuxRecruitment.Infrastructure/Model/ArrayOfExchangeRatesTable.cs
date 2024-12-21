using System.Xml.Serialization;

namespace LuxRecruitment.Infrastructure.Model
{
    [XmlRoot("ArrayOfExchangeRatesTable")]
    public sealed class ArrayOfExchangeRatesTable
    {
        [XmlElement("ExchangeRatesTable")]
        public List<ExchangeRatesTable> ExchangeRatesTables { get; set; }
    }
}
