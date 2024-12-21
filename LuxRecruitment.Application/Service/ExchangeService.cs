using LuxRecruitment.Application.Interfaces;
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

        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync()
        {
            return await _nbpApiService.GetExchangeRatesAsync();
        }
    }
}
