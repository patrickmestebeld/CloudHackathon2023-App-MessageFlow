using MessageFlow.Core.Messaging.Entities;

namespace MessageFlow.Core.Messaging.Interfaces
{
    public interface IPersonalDataFetcher
    {
        PersonalData GetPersoonsGegevens(Guid burgerKey);
    }
}
