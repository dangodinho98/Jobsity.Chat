namespace Jobsity.Chat.Repositories
{
    using Jobsity.Chat.Repositories.Messages;
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstraper
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IMessageRepository, MessageRepository>();
        }
    }
}