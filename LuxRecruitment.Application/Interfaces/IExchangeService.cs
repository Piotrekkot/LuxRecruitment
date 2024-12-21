using LuxRecruitment.Core.Model;

namespace LuxRecruitment.Application.Interfaces
{
    public interface IExchangeService
    {
        Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync();
    }
}
