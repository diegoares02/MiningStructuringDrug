using System.Net;
using MiningStructuringDrug.Infrastructure.Services.DailyMed;
using Moq;
using Moq.Protected;

namespace MiningStructuringDrug.Test.Infrastructure.Services.DailyMed
{
    public class DailyMedServiceTests
    {
        [Fact]
        public async Task GetDrugIndicationsAsync_ValidDrugName_ReturnsIndications()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("INDICATIONS AND USAGE This is a test indication. DOSAGE")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var dailyMedService = new DailyMedService(httpClient);

            // Act
            var indications = await dailyMedService.GetDrugIndicationsAsync("TestDrug");

            // Assert
            Assert.Single(indications);
            Assert.Contains("This is a test indication", indications[0]);
        }

        [Fact]
        public async Task GetDrugIndicationsAsync_InvalidDrugName_ReturnsEmptyList()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var dailyMedService = new DailyMedService(httpClient);

            // Act
            var indications = await dailyMedService.GetDrugIndicationsAsync("InvalidDrug");

            // Assert
            Assert.Empty(indications);
        }

        [Fact]
        public async Task GetDrugLabelAsync_ValidDrugName_ReturnsLabel()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("This is a test label")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var dailyMedService = new DailyMedService(httpClient);

            // Act
            var label = await dailyMedService.GetDrugLabelAsync("TestDrug");

            // Assert
            Assert.Equal("This is a test label", label);
        }

        [Fact]
        public async Task GetDrugLabelAsync_InvalidDrugName_ReturnsNull()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var dailyMedService = new DailyMedService(httpClient);

            // Act
            var label = await dailyMedService.GetDrugLabelAsync("InvalidDrug");

            // Assert
            Assert.Null(label);
        }

        [Fact]
        public async Task GetDrugLabelAsync_HttpRequestException_ReturnsNull()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("Test exception"));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var dailyMedService = new DailyMedService(httpClient);

            // Act
            var label = await dailyMedService.GetDrugLabelAsync("TestDrug");

            // Assert
            Assert.Null(label);
        }
    }
}
