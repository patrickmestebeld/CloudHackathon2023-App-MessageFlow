namespace MessageOracle;

public partial class MessageOracleClient
{
    public string _subscriptionKey;

    public MessageOracleClient(string baseUrl, string subscriptionKey, HttpClient httpClient) : this(baseUrl, httpClient)
    {
        _subscriptionKey = subscriptionKey;
    }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
    }
}
