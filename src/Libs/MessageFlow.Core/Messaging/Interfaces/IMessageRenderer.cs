using MessageFlow.Core.Messaging.Models;

namespace MessageFlow.Core.Messaging.Interfaces
{
    public interface IMessageRenderer
    {
        Message Render(RenderableMessage message);
    }
}
