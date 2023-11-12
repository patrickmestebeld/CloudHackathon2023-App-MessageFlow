namespace MessageFlow.Infra.Messaging.Models
{
    public class InboxMessage
    {
        public InboxMessage(Guid subjectId, string messageType, string url)
        {
            SubjectId = subjectId;
            MessageType = messageType;
            Url = url;
        }

        public Guid SubjectId { get; }
        public string MessageType { get; }
        public string Url { get; }
    }
}
