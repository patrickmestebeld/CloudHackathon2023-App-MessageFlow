using System.Text;
using System.Text.Json;
using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace MessageFlow.Jobs
{
    public class Functions
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly IMessageAssembler _messageAssembler;

        public Functions(IMessageAssembler messageAssembler)
        {
            _messageAssembler = messageAssembler;
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
        }
    }
}
