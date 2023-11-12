using System.Text;
using System.Text.Json;
using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Models;
using MessageFlow.Processor.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MessageFlow.Jobs
{
    public class Functions
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly IMessageAssembler _messageAssembler;
        private readonly IMessageInbox _messageInbox;
        private readonly FunctionsOptions _options;

        public Functions(IMessageAssembler messageAssembler,
            IMessageInbox messageInbox,
            IOptions<FunctionsOptions> options)
        {
            _messageAssembler = messageAssembler;
            _messageInbox = messageInbox;
            _options = options.Value;
        }

        public async Task ProcessServicebus(
            [ServiceBusTrigger("sbq-msgt02-dev-001", Connection = "ServiceBusConnection")] string message, string messageId,
            [Blob("messages/{messageId}.md", FileAccess.Write, Connection = "BlobConnection")] Stream outputBlob, ILogger logger)
        {
            logger.LogInformation($"ServiceBus message: {message}");
            MessageContext? messageContext = JsonSerializer.Deserialize<MessageContext>(message, _jsonSerializerOptions);

            var assembledMessage = await _messageAssembler.AssembleAsync(messageContext);


            var stream = new MemoryStream(Encoding.ASCII.GetBytes(assembledMessage.Content));
            stream.CopyTo(outputBlob);

            var url = $"{_options.BlobConnectionString}/messages/{messageId}.md";
            await _messageInbox.SendAsync(new InboxMessage(assembledMessage.RecipientId, assembledMessage.MessageType, url));
        }
    }
}
