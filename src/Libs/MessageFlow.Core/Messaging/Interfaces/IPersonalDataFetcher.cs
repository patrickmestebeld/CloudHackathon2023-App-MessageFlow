using MessageFlow.Core.Messaging.Entities;

namespace MessageFlow.Core.Messaging.Interfaces
{
    public interface IPersonalDataFetcher
    {
        Task<PersonalData> FetchPersonalDataAsync(Guid key);
    }
}
