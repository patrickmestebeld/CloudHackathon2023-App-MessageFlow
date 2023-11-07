using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace MessageFlow.Infra.Messaging.Services;

public class AzureSecretRetriever : ISecretRetriever
{
    private readonly SecretClientOptions _options = new()
    {
        Retry = {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
    };

    public Task<string> RetrieveSecretAsync(Uri vaultUri, string secretName)
        => new SecretClient(vaultUri, new DefaultAzureCredential(), _options)
            .GetSecretAsync(secretName)
            .ContinueWith(t => t.Result.Value.Value);
}