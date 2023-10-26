using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.ValueObjects;

namespace MessageFlow.UnitTests.Fakes
{
    internal class FakePersoonsgegevensFetcher : IPersonalDataFetcher
    {
        public Task<PersonalData> FetchPersonalDataAsync(Guid key)
            => Task.FromResult(
                new PersonalData(new Guid("93a0a5fe-b34c-47fe-ab56-ab619d0f84b5"), new Bsn("123456782"),
                    new Naam("H.", "de Vries", "Henk"),
                    new Adres("1234AB", 1, "AMSTERDAM", "Kerkstraat"))
            );
    }
}
