namespace Jobsity.Chat.Borders.Configuration
{
    using Jobsity.Chat.Borders.Exceptions;
    using Jobsity.Chat.Borders.Validators;
    using Serilog;

    public class ApplicationConfig
    {
        public string BaseUrl { get; set; }
        public string ConnectionString { get; set; }
        public string GetStockEndpoint { get; set; }

        public void Validate()
        {
            var validationResult = new ApplicationConfigValidator().Validate(this);
            if (validationResult.IsValid) return;

            var errors = validationResult.Errors.Select(c => c.ErrorMessage).ToList();

            Log.Error("Configuration: Contains errors: {@errors}", errors);
            throw new ErrorConfigurationException(string.Join(",", errors));
        }
    }
}
