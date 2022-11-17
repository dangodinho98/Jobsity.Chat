namespace Jobsity.Chat.Borders.Validators
{
    using FluentValidation;
    using Jobsity.Chat.Borders.Configuration;

    public class ApplicationConfigValidator : AbstractValidator<ApplicationConfig>
    {
        public ApplicationConfigValidator()
        {
            RuleFor(config => config.BaseUrl).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
            RuleFor(config => config.ConnectionString).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
            RuleFor(config => config.GetStockEndpoint).NotEmpty().WithMessage(Constants.ErrorMessages.MissingApplicationConfig);
        }
    }
}
