namespace Jobsity.Chat.Borders.Validators
{
    using FluentValidation;
    using Jobsity.Chat.Borders.Configuration;

    public class ApplicationConfigValidator : AbstractValidator<ApplicationConfig>
    {
        public ApplicationConfigValidator()
        {
            RuleFor(config => config.ConnectionString).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);

            When(config => config.StockApi is not null, () =>
            {
                RuleFor(config => config.StockApi!.BaseUrl).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
                RuleFor(config => config.StockApi!.GetStockEndpoint).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
            });

            When(config => config.RabbitMq is not null, () =>
            {
                RuleFor(config => config.RabbitMq!.Hostname).NotEmpty()
                    .WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
            });
        }
    }
}
