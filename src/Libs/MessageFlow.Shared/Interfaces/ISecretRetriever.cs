namespace MessageFlow.Infra.Messaging.Services
{
    public interface ISecretRetriever
    {
        Task<string> RetrieveSecretAsync(Uri vaultUri, string secretName);
    }
}