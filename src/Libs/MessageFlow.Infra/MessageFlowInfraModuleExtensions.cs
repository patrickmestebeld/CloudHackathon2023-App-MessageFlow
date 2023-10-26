using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Configuration;
using MessageFlow.Infra.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageFlow.Infra
{
    public static class MessageFlowInfraModuleExtensions
    {
        public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<MessagingServiceBusOptions>()
                .Bind(config.GetSection(MessagingServiceBusOptions.Name))
                .ValidateDataAnnotations();
            services.AddTransient<IMessageRenderer, ScribanMessageRenderer>();
            services.AddTransient<IMessageEventWatcher, MessageEventWatcher>();
            services.AddTransient<IPersonalDataFetcher, PersonalDataFetcher>();
            return services;
        }
    }
}
