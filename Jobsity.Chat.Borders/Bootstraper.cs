namespace Jobsity.Chat.Borders
{
    using AutoMapper;
    using Jobsity.Chat.Borders.Mappers;
    using Jobsity.Chat.Borders.Validators;
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstraper
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services
                .AddScoped<ApplicationConfigValidator>();
        }

        public static void AddMapperProfile(this IServiceCollection services)
        {
            services.AddSingleton(_ => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper());
        }
    }
}
