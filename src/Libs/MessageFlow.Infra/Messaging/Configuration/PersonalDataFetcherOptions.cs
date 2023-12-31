﻿namespace MessageFlow.Infra.Messaging.Configuration
{
    public class MessageInboxOptions
    {
        public const string Name = "MessageInbox";

        public string ClientBaseUrl { get; set; } = "http://localhost:5000";
        public string ClientSubscriptionKey { get; set; } = "secret";
    }
}
