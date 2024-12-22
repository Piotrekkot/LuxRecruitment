using LuxRecruitment.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LuxRecruitment.Web.Controllers
{
    public class ExchangeWebController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExchangeWebController> _logger;

        public ExchangeWebController(IHttpClientFactory httpClientFactory, ILogger<ExchangeWebController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Brak tokenu. Przekierowanie na stronę logowania.");
                TempData["LoginMessage"] = "Aby otrzymać dostęp do tej strony, należy się zalogować.";
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("ExchangeApi");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("api/exchange/A/5");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var rates = JsonSerializer.Deserialize<List<ExchangeRateViewModel>>(content);
                _logger.LogInformation("Pomyślnie pobrano dane kursów walut.");
                return View(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas pobierania kursów walut.");
                var errorModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = "Wystąpił problem podczas przetwarzania żądania :" + ex.Message
                };

                return View("Error", errorModel);
            }
        }
    }
}
