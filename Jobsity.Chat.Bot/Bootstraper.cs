namespace Jobsity.Chat.Bot
{
    using Jobsity.Chat.Borders.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstraper
    {
        public static void AddBotProcessor(this IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddTransient<IBotProcessor, BotProcessor>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                return new BotProcessor(factory, applicationConfig.StockApi!.GetStockEndpoint!);
            });
        }
    }
}
