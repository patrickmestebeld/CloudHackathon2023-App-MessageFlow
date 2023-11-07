namespace MessageOracle;

public partial class MessageOracleClient
{
    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        request.Headers.Add("Ocp-Apim-Subscription-Key", "");
    }
}
