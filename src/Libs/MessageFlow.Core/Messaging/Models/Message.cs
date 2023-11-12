namespace MessageFlow.Core.Messaging.Models;

public record Message(Guid RecipientId, string Recipient, string MessageType, string Subject, string Content)
{
}