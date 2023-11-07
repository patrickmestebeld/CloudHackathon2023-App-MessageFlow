using MessageFlow.Infra;
using MessageFlow.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageFlow.QueueJobs
{
    internal class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder();
            builder.UseEnvironment(EnvironmentName.Development);
            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();

                // If the key exists in settings, use it to enable Application Insights.
                string instrumentationKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]!;
                if (!string.IsNullOrEmpty(instrumentationKey))
                {
                    b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = instrumentationKey);
                }
            });
            builder.ConfigureWebJobs((context, b) =>
            {
                b.AddAzureStorageCoreServices();
                b.AddServiceBus();
                b.AddAzureStorageBlobs();
            });
            builder.ConfigureServices((context, s) =>
            {
                s.AddCoreModule();
                s.AddInfraWebJobsModule(o =>
                {
                    o.ClientBaseUrl = context.Configuration["PersonalDataFetcher_ClientBaseUrl"]!;
                    o.ClientSubscriptionKey = context.Configuration["PersonalDataFetcher_ClientSubscriptionKey"]!;
                });
                s.AddScoped<Functions>();
            });
            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}