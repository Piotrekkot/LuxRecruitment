using System.Text.Json.Serialization;

namespace LuxRecruitment.Web.Models
{
    public class ExchangeRateViewModel
    {
        [JsonPropertyName("currencyName")]
        public string CurrencyName { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("exchangeRateValue")]
        public decimal ExchangeRateValue { get; set; }
    }
}

