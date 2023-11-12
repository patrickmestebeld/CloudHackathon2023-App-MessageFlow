using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Configuration;
using MessageFlow.Infra.Messaging.Interfaces;
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
            services.AddOptions<MessageInboxOptions>()
                .Bind(config.GetSection(MessageInboxOptions.Name))
                .ValidateDataAnnotations();
            services.AddTransient<IMessageRenderer, ScribanMessageRenderer>();
            services.AddTransient<IPersonalDataFetcher, PersonalDataFetcher>();
            services.AddTransient<IMessageInbox, MessageInbox>();
            return services;
        }

        public static IServiceCollection AddInfraWebJobsModule(this IServiceCollection services,
            Action<PersonalDataFetcherOptions> personalDataConfig,
            Action<MessageInboxOptions> messageInboxConfig)
        {
            services.Configure(personalDataConfig);
            services.Configure(messageInboxConfig);
            services.TryAddSingleton<IMessageRenderer, ScribanMessageRenderer>();
            services.TryAddSingleton<IPersonalDataFetcher, PersonalDataFetcher>();
            services.AddTransient<IMessageInbox, MessageInbox>();
            return services;
        }
    }
}
