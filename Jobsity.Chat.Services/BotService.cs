namespace Jobsity.Chat.Services
{
    using System.Globalization;
    using System.Text;
    using Jobsity.Chat.Borders;

    public class BotService : IBotService
    {
        private readonly IHttpClientFactory _clientFactory;

        public BotService(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<string> GetBotMessage(string message)
        {
            var messages = new StringBuilder();
            try
            {
                var stockCode = message.Replace("/stock=", string.Empty);
                var client = _clientFactory.CreateClient(Constants.StockApiClientName);

                var response = await client.GetAsync(new Uri($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv"));
                response.EnsureSuccessStatusCode();

                var csvText = await response.Content.ReadAsStringAsync();
                var items = MapCsvToItems(csvText);

                foreach (var item in items)
                {
                    messages.Append($"{item.Symbol} quote is ${item.Close} per share");
                }
            }
            catch (Exception)
            {
                messages.Append(Constants.ErrorMessage);
            }

            return messages.ToString();
        }

        private static List<SymbolDto> MapCsvToItems(string csvText)
        {
            var lines = csvText
                .Split(Environment.NewLine)
                .Where(x => !string.IsNullOrEmpty(x));

            var items = lines.Skip(1)
                .Select(line =>
                {
                    var values = line.Split(",");

                    return new SymbolDto()
                    {
                        Symbol = values[0].ToUpper(),
                        Date = DateTime.Parse(values[1]),
                        Time = DateTime.Parse(values[2]),
                        Open = decimal.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture),
                        High = decimal.Parse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Low = decimal.Parse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Close = decimal.Parse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Volume = int.Parse(values[7])
                    };
                })
                .ToList();

            return items;
        }
    }
}