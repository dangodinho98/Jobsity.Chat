namespace Jobsity.Chat.Services.Bot
{
    using CsvHelper;
    using Jobsity.Chat.Borders;
    using Jobsity.Chat.Borders.Configuration;
    using Jobsity.Chat.Borders.Dto;
    using System.Globalization;
    using System.Text;

    public class BotService : IBotService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationConfig _applicationConfig;

        public BotService(IHttpClientFactory httpClientFactory, ApplicationConfig applicationConfig)
        {
            _clientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
        }

        public async Task<string> GetBotMessage(string message)
        {
            var messages = new StringBuilder();
            try
            {
                var stockCode = message.Replace("/stock=", string.Empty);
                var client = _clientFactory.CreateClient(Constants.StockApiClientName);

                var response = await client.GetAsync(string.Format(_applicationConfig.StockApi!.GetStockEndpoint!, stockCode));
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                
                using var reader = new StreamReader(stream);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var items = csv.GetRecords<SymbolDto>();
                
                foreach (var item in items)
                {
                    messages.Append(string.Format(Constants.Bot.Message, item.Symbol, item.Close));
                }
            }
            catch (Exception)
            {
                messages.Append(Constants.ErrorMessages.Default);
            }

            return messages.ToString();
        }
    }
}