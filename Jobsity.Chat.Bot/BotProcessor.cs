namespace Jobsity.Chat.Bot
{
    using Jobsity.Chat.Borders;
    using Jobsity.Chat.Borders.Dto;
    using Jobsity.Chat.Bot.Mappers;
    using System.Globalization;
    using System.Text;
    using CsvHelper;

    public class BotProcessor : IBotProcessor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _endpoint;

        public BotProcessor(IHttpClientFactory httpClientFactory, string endpoint)
        {
            _clientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _endpoint = endpoint;
        }

        public async Task<string> ProcessCommand(string message)
        {
            var messages = new StringBuilder();
            try
            {
                var stockCode = message.Replace("/stock=", string.Empty);
                var client = _clientFactory.CreateClient(Constants.StockApiClientName);

                var response = await client.GetAsync(string.Format(_endpoint, stockCode));
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                using var reader = new StreamReader(stream);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<SymbolDtoMapper>();

                var items = csv.GetRecords<SymbolDto>();

                foreach (var item in items)
                {
                    messages.Append(item.Close == -1
                        ? Constants.ErrorMessages.StockCodeNotFound
                        : string.Format(Constants.Bot.Message, item.Symbol, item.Close));
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