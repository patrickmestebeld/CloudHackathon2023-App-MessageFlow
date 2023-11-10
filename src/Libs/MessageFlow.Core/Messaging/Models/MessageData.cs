using MessageFlow.Core.Messaging.Entities;

namespace MessageFlow.Core.Messaging.Models
{
    public class MessageData
    {
        public PersonalData? Aanvrager { get; init; }
        public bool KgbVariant { get; init; } = false;
        public DateTime DatumDagtekening { get; init; } = default;
        public DateTime DatumVraagbrief => DatumDagtekening.AddDays(-14);
        public DateTime DatumRappelbrief => DatumDagtekening.AddDays(-28);
        public DateTime Reactiedatum { get; init; } = default;
        public int Toeslagjaar { get; init; } = default;
    }
}

