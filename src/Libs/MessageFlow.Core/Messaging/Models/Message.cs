namespace MessageFlow.Core.Messaging.Models;

public record Message(string Recipient, string Subject, string Content)
{
}