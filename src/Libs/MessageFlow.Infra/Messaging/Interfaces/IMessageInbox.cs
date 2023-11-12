using MessageFlow.Infra.Messaging.Models;

namespace MessageFlow.Infra.Messaging.Interfaces
{
    public interface IMessageInbox
    {
        public Task SendAsync(InboxMessage message);
    }
}
