namespace Jobsity.Chat.Tests.Services
{
    using Jobsity.Chat.Borders;
    using Jobsity.Chat.Borders.Configuration;
    using Jobsity.Chat.Bot;
    using Jobsity.Chat.Tests.Builders;
    using Moq;
    using Moq.Protected;
    using System.Net;
    using System.Net.Http;

    public class BotServiceTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly ApplicationConfig _applicationConfig;
        private readonly BotProcessor _processor;

        public BotServiceTest()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>(); 
            _applicationConfig = new ApplicationConfigBuilder()
                .WithBaseUrl("https://test.com/")
                .WithConnectionString("connString")
                .WithGetStockEndpoint("/s={0}")
                .Build();

            _processor = new BotProcessor(_httpClientFactory.Object, _applicationConfig.StockApi!.GetStockEndpoint!);
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
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(csv)
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandler.Object)
            {
                BaseAddress = new Uri(_applicationConfig.StockApi!.BaseUrl!)
            };

            _httpClientFactory.Setup(_ => _.CreateClient(Constants.StockApiClientName))
                .Returns(httpClient).Verifiable();

            var response = await _processor.ProcessCommand("/stock=xxxxxx");
            Assert.Equal(Constants.ErrorMessages.StockCodeNotFound, response);
        }

        [Fact]
        public async Task GetBotMessageWhenUserSendValidStockCodeReturnsBotMessage()
        {
            const string csv = "Symbol,Date,Time,Open,High,Low,Close,Volume\r\nAAPL.US,2022-11-11,22:00:08,145.82,150.01,144.37,149.7,93979665\r\n";

            var httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict){};
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(csv)
                })
            .Verifiable();

            var httpClient = new HttpClient(httpMessageHandler.Object)
            {
                BaseAddress = new Uri(_applicationConfig.StockApi!.BaseUrl!)
            };

            _httpClientFactory.Setup(_ => _.CreateClient(Constants.StockApiClientName))
                .Returns(httpClient).Verifiable();

            var response = await _processor.ProcessCommand("/stock=aapl.us");
            Assert.Equal("AAPL.US quote is $149,7 per share", response);
        }
    }
}