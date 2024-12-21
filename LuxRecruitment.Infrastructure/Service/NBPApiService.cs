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

        public NbpApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["NbpApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl", "Brak BaseUrl w konfiguracji appsettings");
        }

        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
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
                throw new InvalidOperationException("Błąd podczas przetwarzania danych z NBP API.", ex);
            }
        }
    }
}

