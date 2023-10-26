namespace MessageFlow.Infra.Messaging.Configuration;

public class MessagingServiceBusOptions
{
    public const string Name = "MessagingServiceBus";

    public string ConnectionStringVaultUri { get; set; } = default!;
    public string ConnectionStringSecretName { get; set; } = default!;
    public string QueueName { get; set; } = default!;
}
