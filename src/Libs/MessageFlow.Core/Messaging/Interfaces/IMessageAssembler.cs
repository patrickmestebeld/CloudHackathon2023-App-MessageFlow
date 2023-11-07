using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Models;

namespace MessageFlow.Core.Messaging.Interfaces
{
    public interface IMessageAssembler
    {
        Task<Message> AssembleAsync(MessageContext messsageTrigger);
    }
}
