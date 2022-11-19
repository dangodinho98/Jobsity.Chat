namespace Jobsity.Chat.Tests.Builders
{
    using Jobsity.Chat.Borders.Configuration;

    public class ApplicationConfigBuilder
    {
        private readonly ApplicationConfig _instance;

        public ApplicationConfigBuilder()
        {
            _instance = new ApplicationConfig
            {
                StockApi = new StockApi(),
                RabbitMq = new RabbitMq(),
            };
        }

        public ApplicationConfigBuilder WithBaseUrl(string baseUrl)
        {
            _instance.StockApi!.BaseUrl = baseUrl;
            return this;
        }

        public ApplicationConfigBuilder WithConnectionString(string connectionString)
        {
            _instance.ConnectionString = connectionString;
            return this;
        }

        public ApplicationConfigBuilder WithGetStockEndpoint(string getStockEndpoint)
        {
            _instance.StockApi!.GetStockEndpoint = getStockEndpoint;
            return this;
        }

        public ApplicationConfig Build() => _instance;
    }
}
