using LuxRecruitment.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LuxRecruitment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        private readonly ILogger<ExchangeController> _logger;
        public ExchangeController(IExchangeService exchangeService, ILogger<ExchangeController> logger)
        {
            _exchangeService = exchangeService;
            _logger = logger;
        }
        /// <summary>
        /// Pobieranie listy kursów walut z API NBP.
        /// </summary>
        /// <returns>Lista kursów walut</returns>
        [HttpGet]
        public async Task<IActionResult> GetExchangeRates()
        {
            _logger.LogWarning("Start GetExchangeRates");
            try
            {
                var rates = await _exchangeService.GetExchangeRatesAsync();

                if (rates == null || !rates.Any())
                {
                    _logger.LogWarning("Nie znaleziono żadnych kursów walut.");
                    return NotFound(new { Message = "Brak dostępnych kursów walut." });
                }

                _logger.LogInformation("Pobrano {Count} kursów walut.", rates.Count());
                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas pobierania kursów walut.");
                return StatusCode(500, new { Message = "Wystąpił błąd podczas pobierania danych." });
            }
        }
    }
}
