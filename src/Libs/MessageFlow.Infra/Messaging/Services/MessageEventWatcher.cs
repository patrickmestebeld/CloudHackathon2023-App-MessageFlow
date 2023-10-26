using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Configuration;
using Microsoft.Extensions.Options;

namespace MessageFlow.Infra.Messaging.Services;

public class MessageEventWatcher : IMessageEventWatcher
{
    private readonly MessagingServiceBusOptions _serviceBusOptions;

    public MessageEventWatcher(IOptions<MessagingServiceBusOptions> _serviceBusOptions)
    {
        this._serviceBusOptions = _serviceBusOptions.Value;
    }

    public string ExposeConnectionString()
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
        };
        var client = new SecretClient(new Uri(_serviceBusOptions.ConnectionStringVaultLocation), new DefaultAzureCredential(), options);

        KeyVaultSecret secret = client.GetSecret(_serviceBusOptions.ConnectionStringSecretName);

        string secretValue = secret.Value;

        return secretValue;
    }
}