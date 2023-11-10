using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.Models;

namespace MessageFlow.Api.Factories
{
    public class RenderableMessageFactory
    {
        public RenderableMessage CreateFromTriggerAndPerson(MessageContext trigger, PersonalData persoonsGegevens)
        {
            var messageData = new MessageData()
            {
                Aanvrager = persoonsGegevens,
                KgbVariant = trigger.KgbVariant,
                DatumDagtekening = trigger.DatumDagtekening,
                Reactiedatum = trigger.Reactiedatum,
                Toeslagjaar = trigger.Toeslagjaar,
            };

            return new RenderableMessage(trigger.BerichtType, messageData);
        }
    }
}
