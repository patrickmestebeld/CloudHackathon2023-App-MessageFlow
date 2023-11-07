using MessageFlow.Api.Factories;
using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Models;

namespace MessageFlow.Core.Messaging.Services
{
    public class MessageAssembler : IMessageAssembler
    {
        private readonly RenderableMessageFactory _renderableMessageFactory = new();
        private readonly IPersonalDataFetcher _personalDataFetcher;
        private readonly IMessageRenderer _messageRenderer;

        public MessageAssembler(IPersonalDataFetcher personalDataFetcher, IMessageRenderer messageRenderer)
        {
            _personalDataFetcher = personalDataFetcher;
            _messageRenderer = messageRenderer;
        }

        public async Task<Message> AssembleAsync(MessageContext messsageTrigger)
        {
            var personalData = await _personalDataFetcher.FetchPersonalDataAsync(messsageTrigger.AanvragerKey);
            var renderableMessage = _renderableMessageFactory.CreateFromTriggerAndPerson(messsageTrigger, personalData);
            return _messageRenderer.Render(renderableMessage);
        }
    }
}
