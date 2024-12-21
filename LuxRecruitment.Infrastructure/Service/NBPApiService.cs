using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Interfaces;
using LuxRecruitment.Core.Model;
using LuxRecruitment.Infrastructure.Model;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace LuxRecruitment.Infrastructure.Service
{
    public class NbpApiService : INBPApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _exchangeEndpoint;

        public NbpApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["NbpApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl", "Brak BaseUrl w konfiguracji appsettings");
            _exchangeEndpoint = configuration["NbpApi:ExchangeRatesEndpoint"] ?? throw new ArgumentNullException("ExchangeRatesEndpoint", "Brak ExchangeRatesEndpoint w konfiguracji appsettings");
        }

        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync(ExchangeRateTable table, uint topCount)
        {
            try
            {
                var endpoint = _exchangeEndpoint
                .Replace("{tableArg}", table.ToString())
                .Replace("{topCountArg}", topCount.ToString());
                var url = new Uri(new Uri(_baseUrl), endpoint).ToString();

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStreamAsync();

                var serializer = new XmlSerializer(typeof(ArrayOfExchangeRatesTable));
                var result = (ArrayOfExchangeRatesTable)serializer.Deserialize(responseContent);

                var rates = result?.ExchangeRatesTables?
                    .FirstOrDefault()?
                    .Rates?
                    .Select(rate => new ExchangeRateDTO
                    {
                        CurrencyName = rate.Currency,
                        CurrencyCode = rate.Code,
                        ExchangeRateValue = rate.Mid
                    });

                return rates;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Błąd podczas działania API.", ex);
            }
        }
    }
}

