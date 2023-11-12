using MessageFlow.Infra.Messaging.Models;
using MessagingInbox;

namespace MessageFlow.Infra.Messaging.Mapping;

public static class InboxMessageMappingExtensions
{
    public static TslMessage ToTslMessage(this InboxMessage message)
        => new()
        {
            SubjectId = message.SubjectId.ToString(),
            MessageType = message.MessageType,
            Url = message.Url
        };
}