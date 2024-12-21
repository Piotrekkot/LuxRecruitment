using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Model;

namespace LuxRecruitment.Application.Interfaces
{
    public interface IExchangeService
    {
        Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync(ExchangeRateTable table, uint topCount);
    }
}
