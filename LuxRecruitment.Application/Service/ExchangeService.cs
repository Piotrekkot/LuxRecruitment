using LuxRecruitment.Application.Interfaces;
using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Interfaces;
using LuxRecruitment.Core.Model;

namespace LuxRecruitment.Application.Service
{
    public class ExchangeService : IExchangeService
    {
        private readonly INBPApiService _nbpApiService;

        public ExchangeService(INBPApiService nbpApiService)
        {
            _nbpApiService = nbpApiService;
        }

        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync(ExchangeRateTable table, uint topCount)
        {
            return await _nbpApiService.GetExchangeRatesAsync(table,topCount);
        }
    }
}
