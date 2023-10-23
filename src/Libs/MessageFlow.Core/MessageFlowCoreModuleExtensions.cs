using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MessageFlow.Infra
{
    public static class MessageFlowCoreModuleExtensions
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddTransient<IMessageAssembler, MessageAssembler>();
            return services;
        }
    }
}
