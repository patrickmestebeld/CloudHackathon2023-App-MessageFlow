using MessageFlow.Infra.Messaging.Configuration;
using MessageFlow.Infra.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Mapping;
using MessageFlow.Infra.Messaging.Models;
using MessagingInbox;
using Microsoft.Extensions.Options;

namespace MessageFlow.Infra.Messaging.Services
{
    internal class MessageInbox : IMessageInbox
    {
        private readonly MessageInboxOptions _options;

        public MessageInbox(IOptions<MessageInboxOptions> options)
        {
            _options = options.Value;
        }

        public Task SendAsync(InboxMessage message)
            => new MessagingInboxClient(_options.ClientBaseUrl, new HttpClient())
                .MessageAsync(message.ToTslMessage());
    }
}
