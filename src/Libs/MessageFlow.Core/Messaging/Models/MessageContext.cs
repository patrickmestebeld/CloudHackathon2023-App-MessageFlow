namespace MessageFlow.Api.Models
{
    public class MessageContext
    {
        public Guid AanvragerKey { get; set; }
        public bool KgbVariant { get; set; } = false;
        public string BerichtType { get; set; } = "";
        public DateTime DatumDagtekening { get; set; } = default;
        public DateTime Reactiedatum { get; set; } = default;
        public int Toeslagjaar { get; set; } = default;
    }
}
