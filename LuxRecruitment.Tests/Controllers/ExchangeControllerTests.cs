using LuxRecruitment.Application.Interfaces;
using LuxRecruitment.Core.Enums;
using LuxRecruitment.Core.Model;
using LuxRecruitment.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LuxRecruitment.Tests.Controllers
{
    public class ExchangeControllerTests
    {
        private readonly Mock<IExchangeService> _exchangeServiceMock;
        private readonly Mock<ILogger<ExchangeController>> _loggerMock;
        private readonly ExchangeController _controller;

        public ExchangeControllerTests()
        {
            _exchangeServiceMock = new Mock<IExchangeService>();
            _loggerMock = new Mock<ILogger<ExchangeController>>();
            _controller = new ExchangeController(_exchangeServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetExchangeRates_ShouldReturnOkWithRates()
        {
            //Arrange
            var rates = new List<ExchangeRateDTO>
            {
                new ExchangeRateDTO { CurrencyName = "USD", CurrencyCode = "USD", ExchangeRateValue = 4.5m },
                new ExchangeRateDTO { CurrencyName = "EUR", CurrencyCode = "EUR", ExchangeRateValue = 4.7m }
            };
            _exchangeServiceMock
                .Setup(s => s.GetExchangeRatesAsync(ExchangeRateTable.A, 5))
                .ReturnsAsync(rates);

            //Act
            var result = await _controller.GetExchangeRates(ExchangeRateTable.A, 5);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRates = Assert.IsType<List<ExchangeRateDTO>>(okResult.Value);
            Assert.Equal(2, returnedRates.Count);
            Assert.Equal("USD", returnedRates.First().CurrencyCode);
        }

        [Fact]
        public async Task GetExchangeRates_ShouldReturnNotFoundWhenNoRates()
        {
            //Arrange
            _exchangeServiceMock
                .Setup(s => s.GetExchangeRatesAsync(ExchangeRateTable.A, 5))
                .ReturnsAsync(new List<ExchangeRateDTO>());

            //Act
            var result = await _controller.GetExchangeRates(ExchangeRateTable.A, 5);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("Brak dostępnych kursów walut.", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task GetExchangeRates_ShouldReturnServerErrorOnException()
        {
            //Arrange
            _exchangeServiceMock
                .Setup(s => s.GetExchangeRatesAsync(ExchangeRateTable.A, 5))
                .ThrowsAsync(new System.Exception("Test exception"));

            //Act
            var result = await _controller.GetExchangeRates(ExchangeRateTable.A, 5);

            //Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
            Assert.Contains("Wystąpił błąd podczas pobierania danych", errorResult.Value.ToString());
        }
    }
}
