namespace MessageFlow.Processor.Configuration
{
    public class FunctionsOptions
    {
        public static string Name => "FunctionsOptions";

        public string BlobConnectionString { get; set; } = "";
    }
}
