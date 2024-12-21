using LuxRecruitment.Core.Model;

namespace LuxRecruitment.Core.Interfaces
{
    public interface INBPApiService
    {
        Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync();
    }
}
