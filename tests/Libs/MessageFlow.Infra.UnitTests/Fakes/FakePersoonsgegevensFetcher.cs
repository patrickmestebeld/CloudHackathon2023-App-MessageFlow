using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.ValueObjects;

namespace MessageFlow.UnitTests.Fakes
{
    internal class FakePersoonsgegevensFetcher : IPersonalDataFetcher
    {
        public PersonalData GetPersoonsGegevens(Guid burgerKey)
            => new PersonalData(new Bsn("123456782"),
                            new Naam("H.", "de Vries", "Henk"),
                            new Adres("1234AB", 1, "AMSTERDAM", "Kerkstraat"));
    }
}
