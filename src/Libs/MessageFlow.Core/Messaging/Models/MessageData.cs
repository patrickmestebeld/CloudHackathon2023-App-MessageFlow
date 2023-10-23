using MessageFlow.Core.Messaging.Entities;

namespace MessageFlow.Core.Messaging.Models
{
    public class MessageData
    {
        public PersonalData? Aanvrager { get; init; }
        public bool KgbVariant { get; init; } = false;
        public string DatumDagtekening { get; init; } = "";
        public string DatumVraagbrief { get; init; } = "";
        public string Reactiedatum { get; init; } = "";
        public string Toeslagjaar { get; init; } = "";
    }
}

