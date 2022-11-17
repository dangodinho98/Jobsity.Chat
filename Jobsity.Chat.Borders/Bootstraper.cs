namespace Jobsity.Chat.Borders
{
    using Jobsity.Chat.Borders.Validators;
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstraper
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services
                .AddScoped<ApplicationConfigValidator>();
        }
    }
}
