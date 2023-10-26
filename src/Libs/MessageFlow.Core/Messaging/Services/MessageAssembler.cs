using MessageFlow.Api.Factories;
using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Models;

namespace MessageFlow.Core.Messaging.Services
{
    public class MessageAssembler : IMessageAssembler
    {
        private readonly RenderableMessageFactory _renderableMessageFactory = new();
        private readonly IPersonalDataFetcher _persoonsgegevensFetcher;
        private readonly IMessageRenderer _messageRenderer;

        public MessageAssembler(IPersonalDataFetcher persoonsgegevensFetcher, IMessageRenderer messageRenderer)
        {
            _persoonsgegevensFetcher = persoonsgegevensFetcher;
            _messageRenderer = messageRenderer;
        }

        public async Task<Message> AssembleAsync(MessageTrigger messsageTrigger)
        {
            var persoonsGegevens = await _persoonsgegevensFetcher.FetchPersonalDataAsync(messsageTrigger.AanvragerKey);
            var renderableMessage = _renderableMessageFactory.CreateFromTriggerAndPerson(messsageTrigger, persoonsGegevens);
            return _messageRenderer.Render(renderableMessage);
        }
    }
}
