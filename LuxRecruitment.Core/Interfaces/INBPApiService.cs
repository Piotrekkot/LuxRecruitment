using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Model;

namespace LuxRecruitment.Core.Interfaces
{
    public interface INBPApiService
    {
        Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync(ExchangeRateTable table, uint topCount);
    }
}
