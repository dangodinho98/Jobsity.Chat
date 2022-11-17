namespace Jobsity.Chat.Tests
{
    using Jobsity.Chat.Borders;
    using Jobsity.Chat.Services;
    using Moq;
    using Moq.Protected;
    using System.Net;
    using System.Net.Http;

    public class BotServiceTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly BotService _service;

        public BotServiceTest()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _service = new BotService(_httpClientFactory.Object);
        }

        [Fact]
        public async Task GetBotMessageWhenUserSendInvalidStockCodeReturnsDefaultErrorMessage()
        {
            const string csv = "Symbol,Date,Time,Open,High,Low,Close,Volume\r\nXXX,N/D,N/D,N/D,N/D,N/D,N/D,N/D\r\n";

            var httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(csv)
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandler.Object);

            _httpClientFactory.Setup(_ => _.CreateClient(Constants.StockApiClientName))
                .Returns(httpClient).Verifiable();

            var response = await _service.GetBotMessage("/stock=xxxxxx");
            Assert.Equal(Constants.ErrorMessages.Default, response);
        }

        [Fact]
        public async Task GetBotMessageWhenUserSendValidStockCodeReturnsBotMessage()
        {
            const string csv = "Symbol,Date,Time,Open,High,Low,Close,Volume\r\nAAPL.US,2022-11-11,22:00:08,145.82,150.01,144.37,149.7,93979665\r\n";

            var httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(csv)
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandler.Object);

            _httpClientFactory.Setup(_ => _.CreateClient(Constants.StockApiClientName))
                .Returns(httpClient).Verifiable();

            var response = await _service.GetBotMessage("/stock=aapl.us");
            Assert.Equal("AAPL.US quote is $149,7 per share", response);
        }
    }
}