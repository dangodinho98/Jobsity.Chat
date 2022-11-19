namespace Jobsity.Chat.Services
{
    using Jobsity.Chat.Borders;
    using Jobsity.Chat.Borders.Configuration;
    using Jobsity.Chat.Services.Bot;
    using Jobsity.Chat.Services.RabbitMQ;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    public static class Bootstraper
    {
        public static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBotService, BotService>()
                .AddTransient<IConsumer, Consumer>()
                .AddTransient<IProducer, Producer>()
                .AddSignalR();
        }

        public static void AddHttpClients(this IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddHttpClient(Constants.StockApiClientName, c =>
            {
                c.BaseAddress = new Uri(applicationConfig.StockApi!.BaseUrl!);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });
        }
    }
}
