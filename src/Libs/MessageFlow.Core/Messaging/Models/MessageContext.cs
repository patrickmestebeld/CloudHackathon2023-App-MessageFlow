namespace MessageFlow.Api.Models
{
    public class MessageContext
    {
        public Guid AanvragerKey { get; set; }
        public bool KgbVariant { get; set; } = false;
        public string BerichtType { get; set; } = "";
        public string DatumDagtekening { get; set; } = "";
        public string DatumVraagbrief { get; set; } = "";
        public string Reactiedatum { get; set; } = "";
        public string Toeslagjaar { get; set; } = "";
    }
}
