namespace Jobsity.Chat.Borders.Configuration
{
    using Jobsity.Chat.Borders.Exceptions;
    using Jobsity.Chat.Borders.Validators;
    using Serilog;

    public class ApplicationConfig
    {
        public int MessagesShown { get; set; }
        public string? ConnectionString { get; set; }
        public RabbitMq? RabbitMq { get; set; }
        public StockApi? StockApi { get; set; }

        public void Validate()
        {
            var validationResult = new ApplicationConfigValidator().Validate(this);
            if (validationResult.IsValid) return;

            var errors = validationResult.Errors.Select(c => c.ErrorMessage).ToList();

            Log.Error("Configuration: Contains errors: {@errors}", errors);
            throw new ErrorConfigurationException(string.Join(",", errors));
        }
    }

    public class StockApi
    {
        public string? BaseUrl { get; set; }
        public string? GetStockEndpoint { get; set; }
    }

    public class RabbitMq
    {
        public string? Hostname { get; set; }
        public string QueueName { get; set; }
    }
}
