using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MessageFlow.Infra
{
    public static class MessageFlowInfraModuleExtensions
    {
        public static IServiceCollection AddInfraModule(this IServiceCollection services)
        {
            services.AddTransient<IMessageRenderer, ScribanMessageRenderer>();
            services.AddTransient<IPersonalDataFetcher, PersonalDataFetcher>();
            return services;
        }
    }
}
