namespace Jobsity.Chat.Tests.Builders
{
    using Jobsity.Chat.Borders.Configuration;

    public class ApplicationConfigBuilder
    {
        private readonly ApplicationConfig _instance;

        public ApplicationConfigBuilder()
        {
            _instance = new ApplicationConfig();
        }

        public ApplicationConfigBuilder WithBaseUrl(string baseUrl)
        {
            _instance.BaseUrl = baseUrl;
            return this;
        }

        public ApplicationConfigBuilder WithConnectionString(string connectionString)
        {
            _instance.ConnectionString = connectionString;
            return this;
        }

        public ApplicationConfigBuilder WithGetStockEndpoint(string getStockEndpoint)
        {
            _instance.GetStockEndpoint = getStockEndpoint;
            return this;
        }

        public ApplicationConfig Build() => _instance;
    }
}
