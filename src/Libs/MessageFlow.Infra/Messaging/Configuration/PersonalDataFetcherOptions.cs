namespace MessageFlow.Infra.Messaging.Configuration
{
    public class PersonalDataFetcherOptions
    {
        public const string Name = "PersonalDataFetcher";

        public string ClientBaseUrl { get; set; } = "http://localhost:5000";
        public string ClientSubscriptionKey { get; set; } = "secret";
    }
}
