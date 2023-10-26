namespace MessageFlow.Core.Messaging.Interfaces;

public interface IMessageEventWatcher
{
    //Task ExecuteAsync(Action<MessageEvent> action, CancellationToken cancellationToken);
    public string ExposeConnectionString();
}