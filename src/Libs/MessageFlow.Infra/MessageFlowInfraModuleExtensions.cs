using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Configuration;
using MessageFlow.Infra.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessageFlow.Infra
{
    public static class MessageFlowInfraModuleExtensions
    {
        public static IServiceCollection AddInfraApiModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<PersonalDataFetcherOptions>()
                .Bind(config.GetSection(PersonalDataFetcherOptions.Name))
                .ValidateDataAnnotations();
            services.AddTransient<IMessageRenderer, ScribanMessageRenderer>();
            services.AddTransient<IPersonalDataFetcher, PersonalDataFetcher>();
            return services;
        }

        public static IServiceCollection AddInfraWebJobsModule(this IServiceCollection services, Action<PersonalDataFetcherOptions> config)
        {
            services.Configure(config);
            services.TryAddSingleton<IMessageRenderer, ScribanMessageRenderer>();
            services.TryAddSingleton<IPersonalDataFetcher, PersonalDataFetcher>();
            return services;
        }
    }
}
