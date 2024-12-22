using LuxRecruitment.Application.Interfaces;
using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuxRecruitment.WebAPI.Controllers
{
    [Authorize]
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
        /// <param name="table">Typ tabeli kursów walut</param>
        /// <param name="topCount">liczba określająca maksymalny rozmiar zwracanej serii danych</param>
        /// <returns>Lista kursów walut z NBP.</returns>
        /// <response code="200">Sukces - Zwraca listę kursów walut.</response>
        /// <response code="404">Nie znaleziono danych dla podanych parametrów.</response>
        /// <response code="500">Błąd serwera podczas pobierania danych.</response>
        [HttpGet("{table}/{topCount}")]
        [ProducesResponseType(typeof(IEnumerable<ExchangeRateDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetExchangeRates(ExchangeRateTable table, uint topCount)
        {
            _logger.LogWarning("Start GetExchangeRates");
            try
            {
                var rates = await _exchangeService.GetExchangeRatesAsync(table, topCount);

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
